package common

import (
	"bytes"
	"encoding/base64"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
	"os"
	"strings"
)

type DirectoryTree interface {
	InitializeTree(path string) *Tree
	InsertNode(path string, dirEntry os.DirEntry)
	GetRoot() *Node
	TraverseTree(node *Node)
	IsRootNode(path string) bool
}

type Node struct {
	Path     string
	Info     os.DirEntry
	Children map[string]*Node
}

type Tree struct {
	Root       *Node
	GitHubRepo string
}

func (t *Tree) IsRootNode(path string) bool {
	if t.Root.Path == path {
		return true
	}
	return false
}

func (t *Tree) InitializeTree(path string, gitHubUrl string) *Tree {
	treeRoot := new(Tree)
	treeRoot.GitHubRepo = gitHubUrl
	treeRoot.Root = &Node{
		Path:     path,
		Info:     nil,
		Children: make(map[string]*Node),
	}
	return treeRoot
}

func (t *Tree) InsertNode(path string, dirEntry os.DirEntry) {
	nodePaths := strings.Split(strings.TrimPrefix(path, t.Root.Path), BackSlash)
	if t.IsRootNode(path) {
		t.Root.Info = dirEntry
		return
	}
	head := t.Root
	for idx, val := range nodePaths[1:] {
		key := strings.Join(nodePaths[:idx], BackSlash) + BackSlash + val
		if _, ok := head.Children[key]; ok {
			head = head.Children[key]
		} else {
			head.Children[key] = &Node{
				Path:     path,
				Info:     dirEntry,
				Children: make(map[string]*Node),
			}
		}
	}
}

func (t *Tree) GetRoot() *Node {
	return t.Root
}

func (t *Tree) TraverseTree(node *Node) {
	if node == nil {
		return
	}
	vv, err := node.Info.Info()
	if err != nil {
		return
	}
	if !vv.IsDir() {
		//fmt.Println(node.Path)
		paths := strings.Split(node.Path, "/")
		var finPath = ""
		var f = 0
		for i, val := range paths {
			if val == "dronebase" {
				f = 1
			}
			if f == 1 {
				finPath += val
				if i != len(paths)-1 {
					finPath += "/"
				}
			}
		}
		//fmt.Println(finPath)
		CreateRefOnGitHub(finPath, node, t.GitHubRepo)
	}
	for _, val := range node.Children {
		t.TraverseTree(val)
	}
}

func CheckErr(err error) {
	if err != nil {
		fmt.Printf("Error: %v", err.Error())
	}
}

func CreateRefOnGitHub(gitPath string, node *Node, gitHubUrl string) {
	client := &http.Client{}
	data, err := os.ReadFile(node.Path)
	encodedText := base64.StdEncoding.EncodeToString(data)
	val := struct {
		Message string `json:"message"`
		Content string `json:"content"`
	}{
		Message: "backup",
		Content: encodedText,
	}
	jsonData, err := json.Marshal(&val)
	CheckErr(err)
	request, _ := http.NewRequest(http.MethodPut, gitHubUrl+gitPath, bytes.NewReader(jsonData))
	nodeInfo, err := node.Info.Info()
	if nodeInfo.IsDir() {
		return
	}
	request.Header.Set("Accept", "application/vnd.github+json")
	request.Header.Set("Authorization", "Bearer "+os.Getenv("GITHUB_API_KEY"))
	request.Header.Set("X-GitHub-Api-Version", "2022-11-28")
	request.Header.Set("User-Agent", "go-package-http/dev")
	response, err := client.Do(request)
	CheckErr(err)

	if response.StatusCode == 201 {
		return
	} else {
		bytes, _ := ioutil.ReadAll(response.Body)
		fmt.Printf("%v\n", string(bytes))
		fmt.Println("Status Code: ", response.StatusCode)
	}

}

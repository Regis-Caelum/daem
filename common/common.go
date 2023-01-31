package common

import (
	"fmt"
	"log"
	"os"
	"strings"
)

type DirectoryTree interface {
	InitializeTree(path string) *Tree
	InsertNode(path string, dirEntry os.DirEntry)
	GetRoot() *Node
	PrintTree(node *Node)
	IsRootNode(path string) bool
}

type Node struct {
	Path     string
	Info     os.DirEntry
	Children map[string]*Node
}

type Tree struct {
	Root *Node
}

func (t *Tree) IsRootNode(path string) bool {
	if t.Root.Path == path {
		return true
	}
	return false
}

func (t *Tree) InitializeTree(path string) *Tree {
	treeRoot := new(Tree)
	treeRoot.Root = &Node{
		Path:     path,
		Info:     nil,
		Children: make(map[string]*Node),
	}
	return treeRoot
}

func (t *Tree) InsertNode(path string, dirEntry os.DirEntry) {
	nodePaths := strings.Split(strings.TrimPrefix(path, t.Root.Path), "/")
	if t.IsRootNode(path) {
		t.Root.Info = dirEntry
		return
	}
	head := t.Root
	for idx, val := range nodePaths[1:] {
		key := strings.Join(nodePaths[:idx], "/") + "/" + val
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

func (t *Tree) PrintTree(node *Node) {
	if node == nil {
		return
	}
	fmt.Println(node.Path)
	for _, val := range node.Children {
		t.PrintTree(val)
	}
}

func CheckErr(err error) {
	if err != nil {
		log.Printf("Error: %v", err.Error())
	}
}

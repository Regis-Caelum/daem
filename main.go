package main

import (
	"daem/common"
	"io/fs"
	"log"
	"os"
	"path/filepath"
)

func main() {
	path := os.Getenv("root_path")
	gitHubUrl := common.GitCreatePath + os.Getenv("Username") + common.BackSlash + os.Getenv("Repo_Name") + common.BackSlash + common.Contents
	directoryTree := new(common.Tree)
	directoryTree = directoryTree.InitializeTree(path, gitHubUrl)
	log.Println("Generating tree...")
	err := filepath.WalkDir(path, func(path string, d fs.DirEntry, err error) error {
		if err != nil {
			common.CheckErr(err)
			return err
		}
		directoryTree.InsertNode(path, d)
		return nil
	})
	log.Println("Tree generated successfully.")
	common.CheckErr(err)
	log.Println("Printing tree...")
	directoryTree.TraverseTree(directoryTree.GetRoot())
	log.Println("Tree printed successfully.")
}

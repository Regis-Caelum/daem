package main

import (
	"daem/common"
	"io/fs"
	"log"
	"path/filepath"
)

func main() {
	path := "/home/kenkaneki124/dronebase"
	gitHubUrl := "https://api.github.com/repos/AbhayDhaundiyal/Hello-00World/contents/"
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

package main

import (
	"daem/common"
	"io/fs"
	"path/filepath"
)

func main() {

	directoryTree := new(common.Tree)
	directoryTree = directoryTree.InitializeTree("/home/mehmed/Desktop")

	err := filepath.WalkDir("/home/mehmed/Desktop", func(path string, d fs.DirEntry, err error) error {
		if err != nil {
			common.CheckErr(err)
			return err
		}
		//fmt.Println(path)
		directoryTree.InsertNode(path, d)
		return nil
	})
	common.CheckErr(err)
	directoryTree.PrintTree(directoryTree.GetRoot())
}

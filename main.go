package main

import (
	"daem/common"
	"io/fs"
	"path/filepath"
)

func main() {

	var directoryTree common.DirectoryTree
	directoryTree = &common.Tree{Root: &common.Node{
		Path:     "",
		Info:     nil,
		Children: map[string]*common.Node{},
	}}

	directoryTree.InitializeTree(".")

	err := filepath.WalkDir(".", func(path string, d fs.DirEntry, err error) error {
		if err != nil {
			common.CheckErr(err)
			return err
		}
		directoryTree.InsertNode(path, d)
		return nil
	})
	common.CheckErr(err)
	directoryTree.PrintTree(directoryTree.GetRoot())
}

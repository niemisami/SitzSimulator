using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputFileHandler {
	
	public Dictionary<float, char> MappingsDict { get; private set; }

	private const string Path = "Assets/";

	public InputFileHandler(string fileName) {

		if (!fileName.EndsWith(".txt")) fileName += ".txt";
		
		var inputFilePath = Directory.GetFiles(Path, fileName, SearchOption.AllDirectories)[0];
		string[] data = File.ReadAllLines(inputFilePath);
		
		MappingsDict = new Dictionary<float, char>();
		
		foreach (var line in data) {
			var lineData = line.Split("\t".ToCharArray());
			var timeStamp = float.Parse(lineData[0]);
			var input = lineData[2];
			MappingsDict.Add(timeStamp, input[0]);
		}
	}
}
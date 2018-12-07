using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputFileHandler {
	
	public Dictionary<float, char> MappingsDict { get; set; }
	public float Bpm { get; set; }
	public float DurationSeconds { get; set; }
	
	private static string _path = "Assets/";
	private string _inputFilePath;

	public InputFileHandler(string fileName) {
		
		_inputFilePath = Directory.GetFiles(_path, fileName, SearchOption.AllDirectories)[0];
		string[] data = File.ReadAllLines(_inputFilePath);
		
		MappingsDict = new Dictionary<float, char>();
		
		for (var i = 0; i < data.Length; i++) {

//			if (i == 0) {
//				var durationArray = data[i].Split(":".ToCharArray());
//				var minutes = int.Parse(durationArray[0].Trim());
//				var seconds = float.Parse(durationArray[1].Trim());
//
//				DurationSeconds = minutes * 60 + seconds;
//			} 
//
//			else if (i == 1) {
//				Bpm = float.Parse(data[i].Trim());
//			}
//			else {
//				
//			}

			var lineData = data[i].Split("\t".ToCharArray());
			var timeStamp = float.Parse(lineData[0]);
			var input = lineData[2];
			MappingsDict.Add(timeStamp, input[0]);
			
		}
	}
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour {

	//Strings are for the colour/other fact, bools are true if the fact is different for each character version in the set 

	public string CharacterType;
	public int CharacterVersion;

	private string Unique;
	private string Gender;
	private bool Glasses = false;
	private string FacialHairColour = null;
	private bool FacialHairColourU = false;
	private string HairColour = null;
	private bool HairColourU = false;
	private string JacketColour = null;
	private bool JacketColourU = false;
	private string ShirtColour = null;
	private bool ShirtColourU = false;
	private string VestColour = null;
	private bool VestColourU = false;
	private string PantsColour = null;
	private bool PantsColourU = false;
	private string ShortsColour = null;
	private bool ShortsColourU = false;
	private string HatColour = null;
	private bool HatColourU = false;
	private string SuspendersColour = null;
	private bool SuspendersColourU = false;
	private string HeadphonesColour = null;
	private bool HeadphonesColourU = false;
	private string DressColour = null;
	private bool DressColourU = false;
	private string SkirtColour = null;
	private bool SkirtColourU = false;
	private string ApronColour = null;
	private bool ApronColourU = false;
	private string DogColour = null;
	private bool DogColourU = false;
	private string CondimentColour = null;
	private bool CondimentColourU = false;
	private string BunColour = null;
	private bool BunColourU = false;

	//Finds Character type and version based on the name of the active child and the mesh
	private void GetCharacterTypeVersion() {
		foreach (SkinnedMeshRenderer Mesh in GetComponentsInChildren<SkinnedMeshRenderer>()) {
			if (Mesh.gameObject.activeSelf) {
				CharacterType = Mesh.gameObject.name;
				if (Mesh.materials [0].name.Contains("Polygon_City_Characters_Mat_01_")) {
					CharacterVersion = 1;
				} else if (Mesh.materials [0].name.Contains ("Polygon_City_Characters_Mat_02_")) {
					CharacterVersion = 2;
				} else if (Mesh.materials [0].name.Contains ("Polygon_City_Characters_Mat_03_")) {
					CharacterVersion = 3;
				} else if (Mesh.materials [0].name.Contains ("Polygon_City_Characters_Mat_04_")) {
					CharacterVersion = 4;
				}
				return;
			}
		}
	}

	//This should be called if the character is chosen as the contact
	//To get the audio file from the <string1, string2>, do String1 + - + String2
	//Right now, it will pull the unique-to-set, gender, one of the unique-to-sets
	//But it will only pull additional properties in the same order each time
	public Dictionary<string, string> ProvideHints(int Number) {
		Dictionary<string, string> Hints = new Dictionary<string, string> ();
		//Add the unique-to-set, plus the gender
		Hints.Add ("Unique", Unique);
		Hints.Add ("Gender", Gender + Random.Range(1,6));
		//Figure out possible unique-in-set
		Dictionary<string, string> PossibleUniqueInSet = new Dictionary<string, string>();
		if (FacialHairColourU)
			PossibleUniqueInSet.Add("FacialHairColour", FacialHairColour);
		else if	(HairColourU)
			PossibleUniqueInSet.Add("HairColour", HairColour);
		else if (JacketColourU)
			PossibleUniqueInSet.Add("JacketColour", JacketColour);
		else if (ShirtColourU)
			PossibleUniqueInSet.Add("ShirtColour", ShirtColour);
		else if (PantsColourU)
			PossibleUniqueInSet.Add("PantsColour", PantsColour);
		else if (VestColourU)
			PossibleUniqueInSet.Add("VestColour", VestColour);
		else if (ShortsColourU)
			PossibleUniqueInSet.Add("ShortsColour", ShortsColour);
		else if (HatColourU)
			PossibleUniqueInSet.Add("HatColour", HatColour);
		else if (SuspendersColourU)
			PossibleUniqueInSet.Add("SuspendersColour", SuspendersColour);
		else if (HeadphonesColourU)
			PossibleUniqueInSet.Add("HeadphonesColour", HeadphonesColour);
		else if (DressColourU)
			PossibleUniqueInSet.Add("DressColour", DressColour);
		else if (SkirtColourU)
			PossibleUniqueInSet.Add("SkirtColour", SkirtColour);
		else if (ApronColourU)
			PossibleUniqueInSet.Add("ApronColour", ApronColour);
		else if (DogColourU)
			PossibleUniqueInSet.Add("DogColour", DogColour);
		else if (CondimentColourU)
			PossibleUniqueInSet.Add("CondimentColourU", CondimentColour);
		else if (BunColourU)
			PossibleUniqueInSet.Add("BunColour", BunColour);

		Dictionary<string, string> AllNonSetUnique = new Dictionary<string, string>();
		if (FacialHairColour != null)
			AllNonSetUnique.Add ("FacialHairColour", FacialHairColour);
		if (HairColour != null)
			AllNonSetUnique.Add ("HairColour", HairColour);
		if (JacketColour != null)
			AllNonSetUnique.Add ("JacketColour", JacketColour);
		if (ShirtColour != null)
			AllNonSetUnique.Add ("ShirtColour", ShirtColour);
		if (VestColour != null)
			AllNonSetUnique.Add ("VestColour", VestColour);
		if (PantsColour != null)
			AllNonSetUnique.Add ("PantsColour", PantsColour);
		if (ShortsColour != null)
			AllNonSetUnique.Add ("ShortsColour", ShortsColour);
		if (HatColour != null)
			AllNonSetUnique.Add ("HatColour", HatColour);
		if (SuspendersColour != null)
			AllNonSetUnique.Add ("SuspendersColour", SuspendersColour);
		if (HeadphonesColour != null)
			AllNonSetUnique.Add ("HeadphonesColour", HeadphonesColour);
		if (DressColour != null)
			AllNonSetUnique.Add ("DressColour", DressColour);
		if (ApronColour != null)
			AllNonSetUnique.Add ("ApronColour", ApronColour);
		if (DogColour != null)
			AllNonSetUnique.Add ("DogColour", DogColour);
		if (CondimentColour != null)
			AllNonSetUnique.Add ("CondimentColour", CondimentColour);
		if (BunColour != null)
			AllNonSetUnique.Add ("BunColour", BunColour);

		int r = Random.Range (1, PossibleUniqueInSet.Count);
		int i = 1;
		foreach (KeyValuePair<string, string> element in PossibleUniqueInSet) {
			if (i == r) {
				//Add hint to Hints, remove element as option in AllSetNonUnique
				Hints.Add (element.Key, element.Value);
				Dictionary<string, string> TempAllSetNonUnique = new Dictionary<string, string> ();
				foreach (KeyValuePair<string, string> Property in AllNonSetUnique) {
					if (Property.Key != element.Key) {
						TempAllSetNonUnique.Add (Property.Key, Property.Value);
					}
				}
				AllNonSetUnique.Clear ();
				foreach (KeyValuePair<string, string> ToAdd in TempAllSetNonUnique) {
					AllNonSetUnique.Add (ToAdd.Key, ToAdd.Value);
				}
				TempAllSetNonUnique.Clear ();
				break;
			}
			i++;
		};

		//Before we go in, we should have three hints. The gender, the unique-to-set, and a unique-in-set

		foreach (KeyValuePair<string, string> Candidate in AllNonSetUnique) {
			Hints.Add (Candidate.Key, Candidate.Value);
		}

		//The first three should be chosen every time, but this is where it would be nice (TODO) to randomize the rest
		Dictionary<string, string> FinalHints = new Dictionary<string, string> ();
		int k = 0;
		foreach (KeyValuePair<string, string> Hint in Hints) {
			FinalHints.Add(Hint.Key, Hint.Value);
			k++;
			if (k >= Number) {
				break;
			}
		}

		return FinalHints;
		//Now, we should have a set of hints based on the Number parameter that is guaranteed to point to one version of one type of character
	}

	private void DebugLogHints(Dictionary<string, string> HintsToLog) {
		foreach (KeyValuePair<string, string> HintToLog in HintsToLog) {
			Debug.Log ("Hint Type: " + HintToLog.Key + ", Hint Specific: " + HintToLog.Value);
		}
	}

	//First get character type, set unique and gender, set unique-in-set bools
	//Then set other properties based on version

	private void SetCharacterProfile() {
		if (CharacterType == "Character_Biker") {
			Unique = "Sunglasses";
			Gender = "Male";
			Glasses = true;
			JacketColourU = true;
			if (CharacterVersion == 1) {
				FacialHairColour = "Grey";
				JacketColour = "Black";
				ShirtColour = "White";
				PantsColour = "Blue";
			} else if (CharacterVersion == 2) {
				FacialHairColour = "Blonde";
				JacketColour = "Green";
				ShirtColour = "Grey";
				PantsColour = "Black";
			} else if (CharacterVersion == 3) {
				FacialHairColour = "Brown";
				JacketColour = "Brown";
				ShirtColour = "White";
				PantsColour = "Brown";	
			} else if (CharacterVersion == 4) {
				FacialHairColour = "Brown";
				JacketColour = "Blue";
				ShirtColour = "White";
				PantsColour = "Blue";
			}
		} else if (CharacterType == "Character_FastFoodGuy") {
			Unique = "NameTag";
			Gender = "Male";
			ShirtColourU = true;
			HatColourU = true;
			if (CharacterVersion == 1) {
				FacialHairColour = "Brown";
				HairColour = "Brown";
				HatColour = "Yellow";
				ShirtColour = "Yellow";
				PantsColour = "Blue";
			} else if (CharacterVersion == 2) {
				FacialHairColour = "Brown";
				HairColour = "Brown";
				HatColour = "Maroon";
				ShirtColour = "Maroon";
				PantsColour = "Black";
			} else if (CharacterVersion == 3) {
				FacialHairColour = "Black";
				HairColour = "Black";
				HatColour = "Maroon";
				ShirtColour = "Maroon";
				PantsColour = "Brown";
			} else if (CharacterVersion == 4) {
				FacialHairColour = "Brown";
				HairColour = "Brown";
				HatColour = "Brown";
				ShirtColour = "Brown";
				PantsColour = "Grey";
			}
		} else if (CharacterType == "Character_FireFighter") {
			Unique = "Firefighter";
			Gender = "Male";
			JacketColourU = true;
			PantsColourU = true;
			if (CharacterVersion == 1) {
				HatColour = "Red";
				JacketColour = "Black";
				PantsColour = "Black";
			} else if (CharacterVersion == 2) {
				HatColour = "Yellow";
				JacketColour = "Red";
				PantsColour = "Red";
			} else if (CharacterVersion == 3) {
				HatColour = "Black";
				JacketColour = "Yellow";
				PantsColour = "Yellow";
			} else if (CharacterVersion == 4) {
				HatColour = "Yellow";
				JacketColour = "Orange";
				PantsColour = "Orange";
			}
		} else if (CharacterType == "Character_GamerGirl") {
			Unique = "Headphones";
			Gender = "Female";
			JacketColourU = true;
			HairColourU = true;
			HeadphonesColourU = true;
			Glasses = true;
			if (CharacterVersion == 1) {
				JacketColour = "Purple";
				HairColour = "Cyan";
				ShirtColour = "White";
				PantsColour = "Black";
				HeadphonesColour = "Purple";
			} else if (CharacterVersion == 2) {
				JacketColour = "Maroon";
				HairColour = "Brown";
				ShirtColour = "White";
				PantsColour = "Black";
				HeadphonesColour = "Orange";
			} else if (CharacterVersion == 3) {
				JacketColour = "Cyan";
				HairColour = "Purple";
				ShirtColour = "White";
				PantsColour = "Black";
				HeadphonesColour = "Green";
			} else if (CharacterVersion == 4) {
				JacketColour = "Orange";
				HairColour = "Red";
				ShirtColour = "White";
				PantsColour = "Black";
				HeadphonesColour = "Blue";
			}
		} else if (CharacterType == "Character_Gangster") {
			Unique = "BaseballCap";
			Gender = "Male";
			VestColourU = true;
			HatColourU = true;
			if (CharacterVersion == 1) {
				FacialHairColour = "Brown";
				VestColour = "Green";
				HatColour = "Green";
				PantsColour = "Black";
			} else if (CharacterVersion == 2) {
				FacialHairColour = "Black";
				VestColour = "Blue";
				HatColour = "Blue";
				PantsColour = "Black";
			} else if (CharacterVersion == 3) {
				FacialHairColour = "Black";
				VestColour = "Orange";
				HatColour = "Orange";
				PantsColour = "Black";
			} else if (CharacterVersion == 4) {
				FacialHairColour = "Brown";
				VestColour = "Purple";
				HatColour = "Purple";
				PantsColour = "Black";
			}
		} else if (CharacterType == "Character_Grandma") {
			Unique = "Dress";
			Gender = "Female";
			Glasses = true;
			HairColour = "Grey";
			DressColourU = true;
			if (CharacterVersion == 1) {
				DressColour = "Grey";
			} else if (CharacterVersion == 2) {
				DressColour = "Yellow";
			} else if (CharacterVersion == 3) {
				DressColour = "Maroon";
			} else if (CharacterVersion == 4) {
				DressColour = "Black";
			}
		} else if (CharacterType == "Character_Grandpa") {
			Unique = "HatAndGlasses";
			Gender = "Male";
			Glasses = true;
			JacketColourU = true;
			if (CharacterVersion == 1) {
				HatColour = "Blue";
				JacketColour = "Brown";
				PantsColour = "Black";
				ShirtColour = "Blue";
			} else if (CharacterVersion == 2) {
				HatColour = "Brown";
				JacketColour = "Green";
				PantsColour = "Black";
				ShirtColour = "Red";
			} else if (CharacterVersion == 3) {
				HatColour = "Maroon";
				JacketColour = "Grey";
				PantsColour = "Black";
				ShirtColour = "Grey";
			} else if (CharacterVersion == 4) {
				HatColour = "Blue";
				JacketColour = "White";
				PantsColour = "Black";
				ShirtColour = "Blue";
			}
		} else if (CharacterType == "Character_HipsterGirl") {
			Unique = "StripedShirt";
			Gender = "Female";
			Glasses = true;
			SuspendersColourU = true;
			SkirtColourU = true;
			if (CharacterVersion == 1) {
				HatColour = "Blue";
				SuspendersColour = "Red";
				SkirtColour = "Red";
				PantsColour = "Black";
				HairColour = "Blonde";
			} else if (CharacterVersion == 2) {
				HatColour = "Grey";
				SuspendersColour = "Blue";
				SkirtColour = "Blue";
				PantsColour = "Black";
				HairColour = "Brown";
			} else if (CharacterVersion == 3) {
				HatColour = "Maroon";
				SuspendersColour = "Green";
				SkirtColour = "Green";
				PantsColour = "Black";
				HairColour = "Blonde";
			} else if (CharacterVersion == 4) {
				HatColour = "Blue";
				SuspendersColour = "Black";
				SkirtColour = "Black";
				PantsColour = "Black";
				HairColour = "Black";
			}
		} else if (CharacterType == "Character_HipsterGuy") {
			Unique = "Bowtie";
			Gender = "Male";
			HairColourU = true;
			SuspendersColourU = true;
			ShirtColourU = true;
			PantsColour = "Black";
			if (CharacterVersion == 1) {
				FacialHairColour = "Brown";
				HairColour = "Brown";
				ShirtColour = "Blue";
				SuspendersColour = "Red";
			} else if (CharacterVersion == 2) {
				FacialHairColour = "Brown";
				HairColour = "Brown";
				ShirtColour = "Black";
				SuspendersColour = "Blue";
			} else if (CharacterVersion == 3) {
				FacialHairColour = "Purple";
				HairColour = "Purple";
				ShirtColour = "Yellow";
				SuspendersColour = "Green";
			} else if (CharacterVersion == 4) {
				FacialHairColour = "Blue";
				HairColour = "Blue";
				ShirtColour = "Blue";
				SuspendersColour = "Black";
			}
		} else if (CharacterType == "Character_Hobo") {
			Unique = "TruckerHat";
			PantsColour = "Brown";
			FacialHairColourU = true;
			JacketColourU = true;
			ShirtColourU = true;
			Gender = "Male";
			if (CharacterVersion == 1) {
				HatColour = "Blue";
				JacketColour = "Blue";
				FacialHairColour = "Grey";
				ShirtColour = "Green";
			} else if (CharacterVersion == 2) {
				HatColour = "Red";
				JacketColour = "Black";
				FacialHairColour = "Blonde";
				ShirtColour = "Brown";
			} else if (CharacterVersion == 3) {
				HatColour = "Green";
				JacketColour = "Brown";
				FacialHairColour = "Brown";
				ShirtColour = "Purple";
			} else if (CharacterVersion == 4) {
				HatColour = "Blue";
				JacketColour = "Blue";
				FacialHairColour = "Red";
				ShirtColour = "Maroon";
			}
		} else if (CharacterType == "Character_Jock") {
			Unique = "LetterJacket";
			JacketColourU = true;
			ShirtColour = "Black";
			Gender = "Male";
			if (CharacterVersion == 1) {
				JacketColour = "Red";
				PantsColour = "Blue";
				HairColour = "Blonde";
			} else if (CharacterVersion == 2) {
				JacketColour = "Green";
				PantsColour = "Black";
				HairColour = "Brown";
			} else if (CharacterVersion == 3) {
				JacketColour = "Blue";
				PantsColour = "Brown";
				HairColour = "Blonde";
			} else if (CharacterVersion == 4) {
				JacketColour = "Purple";
				PantsColour = "Blue";
				HairColour = "Black";
			}
		} else if (CharacterType == "Character_Hotdog") {
			Unique = "Hotdog";
			CondimentColourU = true;
			BunColourU = true;
			DogColourU = true;
			ShirtColourU = true;
			Gender = "Male";
			if (CharacterVersion == 1) {
				CondimentColour = "Yellow";
				BunColour = "Mustard";
				PantsColour = "Blue";
				ShirtColour = "Green";
				DogColour = "Red";
			} else if (CharacterVersion == 2) {
				CondimentColour = "Red";
				BunColour = "Brown";
				PantsColour = "Black";
				ShirtColour = "Brown";
				DogColour = "Brown";
			} else if (CharacterVersion == 3) {
				CondimentColour = "Pink";
				BunColour = "Orange";
				PantsColour = "Brown";
				ShirtColour = "Purple";
				DogColour = "Pink";
			} else if (CharacterVersion == 4) {
				CondimentColour = "Green";
				BunColour = "Yellow";
				PantsColour = "Blue";
				ShirtColour = "Maroon";
				DogColour = "Purple";
			}
		} else if (CharacterType == "Character_Paramedic") {
			Unique = "UniformNoHat";
			ShirtColourU = true;
			PantsColourU = true;
			Gender = "Female";
			if (CharacterVersion == 1) {
				ShirtColour = "Cyan";
				PantsColour = "Cyan";
				HairColour = "Blonde";
			} else if (CharacterVersion == 2) {
				ShirtColour = "Blue";
				PantsColour = "Blue";
				HairColour = "Brown";
			} else if (CharacterVersion == 3) {
				ShirtColour = "Green";
				PantsColour = "Green";
				HairColour = "Blonde";
			} else if (CharacterVersion == 4) {
				ShirtColour = "Black";
				PantsColour = "Black";
				HairColour = "Black";
			}
		} else if (CharacterType == "Character_PunkGirl") {
			Unique = "Mohawk";
			Gender = "Female";
			ShortsColourU = true;
			if (CharacterVersion == 1) {
				ShirtColour = "White";
				HairColour = "Brown";
				ShortsColour = "Blue";
			} else if (CharacterVersion == 2) {
				ShirtColour = "White";
				HairColour = "Purple";
				ShortsColour = "Purple";
			} else if (CharacterVersion == 3) {
				ShirtColour = "Cyan";
				HairColour = "Purple";
				ShortsColour = "Brown";
			} else if (CharacterVersion == 4) {
				ShirtColour = "Purple";
				HairColour = "Blue";
				ShortsColour = "Black";
			}
		} else if (CharacterType == "Character_PunkGuy") {
			Unique = "Mohawk";
			Gender = "Male";
			JacketColourU = true;
			HairColourU = true;
			ShirtColour = "White";
			if (CharacterVersion == 1) {
				HairColour = "Red";
				JacketColour = "Black";
			} else if (CharacterVersion == 2) {
				HairColour = "Blue";
				JacketColour = "Maroon";
			} else if (CharacterVersion == 3) {
				HairColour = "Green";
				JacketColour = "Blue";
			} else if (CharacterVersion == 4) {
				HairColour = "Black";
				JacketColour = "Brown";
			} 
		} else if (CharacterType == "Character_RoadWorker") {
			Unique = "SafetyHelmet";
			Gender = "Male";
			JacketColourU = true;
			ShirtColourU = true;
			HatColourU = true;
			if (CharacterVersion == 1) {
				JacketColour = "Yellow";
				PantsColour = "Blue";
				ShirtColour = "Mustard";
				HatColour = "Yellow";
			} else if (CharacterVersion == 2) {
				JacketColour = "Orange";
				PantsColour = "Black";
				ShirtColour = "Maroon";
				HatColour = "Maroon"; 
			} else if (CharacterVersion == 3) {
				JacketColour = "Blue";
				PantsColour = "Brown";
				ShirtColour = "Green";
				HatColour = "Green"; 
			} else if (CharacterVersion == 4) {
				JacketColour = "Orange";
				PantsColour = "Blue";
				ShirtColour = "Brown";
				HatColour = "Brown"; 
			}
		} else if (CharacterType == "Character_ShopKeeper") {
			Unique = "Apron";
			Gender = "Female";
			PantsColour = "Black";
			ApronColourU = true;
			ShirtColourU = true;
			HatColourU = true;
			if (CharacterVersion == 1) {
				ApronColour = "Yellow";
				ShirtColour = "Red";
				HatColour = "Yellow";
				HairColour = "Blonde";
			} else if (CharacterVersion == 2) {
				ApronColour = "Maroon";
				ShirtColour = "Yellow";
				HatColour = "Maroon";
				HairColour = "Brown";
			} else if (CharacterVersion == 3) {
				ApronColour = "Green";
				ShirtColour = "White";
				HatColour = "Green";
				HairColour = "Blonde";
			} else if (CharacterVersion == 4) {
				ApronColour = "Brown";
				ShirtColour = "Orange";
				HatColour = "Brown";
				HairColour = "Black";
			}
		} else if (CharacterType == "Character_SummerGirl") {
			Unique = "SunglassesOnHat";
			Gender = "Female";
			ShirtColourU = true;
			ShortsColourU = true;
			if (CharacterVersion == 1) {
				ShirtColour = "Purple";
				ShortsColour = "Blue";
			} else if (CharacterVersion == 2) {
				ShirtColour = "Maroon";
				ShortsColour = "Black";
			} else if (CharacterVersion == 3) {
				ShirtColour = "Cyan";
				ShortsColour = "Brown";
			} else if (CharacterVersion == 4) {
				ShirtColour = "Orange";
				ShortsColour = "Purple";
			}
		} else if (CharacterType == "Character_Tourist") {
			Unique = "Camera";
			Gender = "Male";
			ShortsColour = "Brown";
			HatColourU = true;
			ShirtColourU = true;
			if (CharacterVersion == 1) {
				HatColour = "Brown";
				ShirtColour = "Orange";
			} else if (CharacterVersion == 2) {
				HatColour = "Yellow";
				ShirtColour = "Blue";
			} else if (CharacterVersion == 3) {
				HatColour = "Maroon";
				ShirtColour = "Green";
			} else if (CharacterVersion == 4) {
				HatColour = "Grey";
				ShirtColour = "Purple";
			}
		} else {
			Debug.Log ("CHARACTER TYPE UNSUPPORTED");
		}
	}
		
	// Use this for initialization
	void Start () {
		GetCharacterTypeVersion ();
		SetCharacterProfile ();
		//DebugLogHints(ProvideHints (5));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
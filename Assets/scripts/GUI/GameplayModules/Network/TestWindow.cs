using UnityEngine;
using System.Collections;

public class TestWindow {

	private bool doneTesting = true;
	private bool useNat = false;
	private string testMessage = "this is a test message";
	private ConnectionTesterStatus testResult = ConnectionTesterStatus.Undetermined;
	
	public void PrintGUI(){
		if(doneTesting && GUILayout.Button("Start Test")){ //new Rect(0,0,position.width/2,20),
			doneTesting = false;
			testResult = ConnectionTesterStatus.Undetermined;
		}else if(!doneTesting){
			GUILayout.Box("Testing...");
		}
		GUILayout.Label(testMessage);
		if(!doneTesting && Event.current.type == EventType.Layout){
			TestConnection();
		}if(doneTesting){
		
			GUILayout.Label("use of nat recommended: "+ (useNat ? "yes" : "no"));
		}
		
	}
	
	private void TestConnection(){
		testResult = Network.TestConnection();
		switch(testResult){
			case ConnectionTesterStatus.Error: 
				testMessage = "Problem determining NAT capabilities";
	       	    doneTesting = true;
        	    break;
            
	        case ConnectionTesterStatus.Undetermined: 
	            testMessage = "Undetermined NAT capabilities";
	            doneTesting = false;
	            break;
	                        
	        case ConnectionTesterStatus.PublicIPIsConnectable:
	            testMessage = "Directly connectable public IP address.";
	            useNat = false;
	            doneTesting = true;
	            break;
	            
	        // This case is a bit special as we now need to check if we can 
	        // circumvent the blocking by using NAT punchthrough
	        case ConnectionTesterStatus.PublicIPPortBlocked:
	            testMessage = "Non-connectable public IP address, running a server is impossible.";
	            useNat = false;
	            // If no NAT punchthrough test has been performed on this public 
	            // IP, force a test
//	            if (!probingPublicIP) {
//	                connectionTestResult = Network.TestConnectionNAT();
//	                probingPublicIP = true;
//	                testStatus = "Testing if blocked public IP can be circumvented";
//	                timer = Time.time + 10;
//	            }
//	            // NAT punchthrough test was performed but we still get blocked
//	            else if (Time.time > timer) {
//	                probingPublicIP = false;         // reset
//	                useNat = true;
//	                doneTesting = true;
//	            }
	            break;
	        case ConnectionTesterStatus.PublicIPNoServerStarted:
	            testMessage = "Public IP address but server not initialized, "+
	                "it must be started to check server accessibility. Restart "+
	                "connection test when ready.";
	            break;
	                        
	        case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
	            testMessage = "Limited NAT punchthrough capabilities (Port Restricted). Cannot "+
	                "connect to all types of NAT servers. Running a server "+
	                "is ill advised as not everyone can connect.";
	            useNat = true;
	            doneTesting = true;
	            break;
	            
	        case ConnectionTesterStatus.LimitedNATPunchthroughSymmetric:
	            testMessage = "Limited NAT punchthrough capabilities (Symmetric). Cannot "+
	                "connect to all types of NAT servers. Running a server "+
	                "is ill advised as not everyone can connect.";
	            useNat = true;
	            doneTesting = true;
	            break;
	        
	        case ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone:
	        case ConnectionTesterStatus.NATpunchthroughFullCone:
	            testMessage = "NAT punchthrough capable. Can connect to all "+
	                "servers and receive connections from all clients. Enabling "+
	                "NAT punchthrough functionality.";
	            useNat = true;
	            doneTesting = true;
	            break;
	
	        default: 
	            testMessage = "Error in test routine, got " + testResult;
			break;
	    }
	}
}

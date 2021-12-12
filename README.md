# RV-Devion
A basic integration of RV-HonorAi and Devion Games Inventory System assets which allows player characters to attack and be attacked by NPCs. The assets are are available from the Unity Asset store.
You will also need the asset export from [RVExt](https://github.com/adrianre12/RV)

## Demo
After downloading this project install the RV-SmartAi, RV-HonorAi, RVExt and Inventory System assets. 
The Demo scenes;
- Attack: Has a player character that can fight with a NPC.
- Interaction: A wandering Vendor and a quest that triggers a behaviour change in the NPC. 

## Usage
On a Devion player character add the PlayerBridge component, then set the Health and Attack stat names to those used by your character. The Group and ReleationshipSystem should be set as normal for HonorAi.

On the NPC add the StatsHandler and AiBridge components. Set the StatsHandler name to "NPC Stats" and add the stat Health from the NPCstats database.


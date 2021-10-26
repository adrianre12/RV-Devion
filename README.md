# RV-Devion
A basic integration of RV-HonorAi and Devion Games Inventory System assets which allows player characters to attack and be attacked by NPCs. The assets are are available from the Unity Asset store.

## Demo
After downloading this project install the RV-SmartAi, RV-HonorAi and Inventory System assets. The Demo scene has a player character that can fight with a NPC. 

## Usage
On a Devion player character add the PlayerBridge component, then set the Health and Attack stat names to those used by your character. The Group and ReleationshipSystem should be set as normal for HonorAi.

On the NPC add the StatsHandler and AiBridge components. Set the StatsHandler name to "NPC Stats" and add the stat Health from the NPCstats database.


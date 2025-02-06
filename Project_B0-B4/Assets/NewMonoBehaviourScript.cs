using UnityEngine;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // 7. Global Variables
    int health = 40;
    int mana = 25;
    int level = 5;
    float strength = 15.5f;
    string playerName = "Bbok";
    bool isFullLevel = false;
    int exp = 1800;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("Hello, Unity!");

        /*/ 1.1 Simple Questions
        Debug.Log("Name of Player");
        Debug.Log(playerName);
        Debug.Log("Level of Player");
        Debug.Log(level);
        Debug.Log("Strength of Player");
        Debug.Log(strength);
        Debug.Log("Whether Player is Full Level");
        Debug.Log(isFullLevel);*/

        // 2. Group Variables
        string[] monsters = { "Slime", "Desert Snake", "Devil" };
        int[] monsterLevel = new int[3];
        monsterLevel[0] = 1;
        monsterLevel[1] = 5;
        monsterLevel[2] = 10;
        // 2.1 Name of monsters
        /*Debug.Log("Monsters existing in this map");
        Debug.Log(monsters[0]);
        Debug.Log(monsters[1]);
        Debug.Log(monsters[2]);
        // 2.2 Level of monsters
        Debug.Log("Each Level of monster");
        Debug.Log(monsterLevel[0]);
        Debug.Log(monsterLevel[1]);
        Debug.Log(monsterLevel[2]);*/

        // 3. Items(Generic)
        List<string> items = new List<string>();
        items.Add("HealthPotion 30");
        items.Add("ManaPotion 30");
        // 3.1 Remove Items
        //items.RemoveAt(0);

        /*Debug.Log("Items that player has");
        Debug.Log(items[0]);*/
        // Index Error
        // Debug.Log(items[1]);

        // 4. Operation

        // 4.1 Update 'exp' of player
        exp += 320;
        exp -= 10;
        level = exp / 300;
        strength = level * 3.141f;

        /*Debug.Log("Total Exp of Player");
        Debug.Log(exp);
        Debug.Log("Level of Player");
        Debug.Log(level);
        Debug.Log("Strength of Player");
        Debug.Log(strength);
        // 4.2 Rest exp for next level
        int nextExp = 300 - (exp % 300);
        Debug.Log("Rest exp for next level");
        Debug.Log(nextExp);
        // 4.3 Name Styling
        string title = "Legend";
        Debug.Log("Name of Player");
        Debug.Log(title + " " + playerName);*/
        // 4.4 Whether Player is Full Level
        int fullLevel = 99;
        isFullLevel = level == fullLevel;
        //Debug.Log("Whether Player is Full Level: " + isFullLevel);
        // 4.5 Whether Player has been in Tutorial
        bool isEndTutorial = level >= 10;
        //Debug.Log("Whether Player has been in Tutorial: " + isEndTutorial);
        // 4.6 Whether Player has been 'Bad Condition'

        bool isBadCondition = health <= 30 && mana <= 15;
        //Debug.Log("Whether Player has been 'Bad Condition': " + isBadCondition);
        // 4.7 Current Condition
        string currCondition = isBadCondition ? "Bad" : "Good";
        //Debug.Log("Current Condition: " + currCondition);

        // 5. Conditional Sentence
        /*if (currCondition == "Bad")
        {
            Debug.Log("Bad Condition. Player has to use item.");
        }   
        else
        {
            Debug.Log("Good Condition. Player hasn't to use item.");
        }*/
        // 5.1 Automatically Use Potions in Bad Condition(if-else)
        //isBadCondition = true;
        /*if (isBadCondition && items[0] == "HealthPotion 30")
        {
            items.RemoveAt(0);
            health += 30;
            Debug.Log("Player uses 'HealthPotion 30' to fill health");
        }
        else if (isBadCondition && items[0] == "ManaPotion 30")
        {
            items.RemoveAt(0);
            mana += 30;
            Debug.Log("Player uses 'ManaPotion 30' to fill mana");
        }*/
        // 5.2 Assign the size of monsters(swtich-case)
        /*switch(monsters[2])
        {
            case "Slime":
            case "Desert Snake":
                Debug.Log("Small Monster came out!");
                break;
            case "Devil":
                Debug.Log("Medium Monster came out!");
                break;
            case "Golem":
                Debug.Log("Big Monster came out!");
                break;
            default:
                Debug.Log("??? Monster came out!");
                break;
        }*/

        // 6. Loop Setence
        // 6.1 while
        /*while (health > 0)
        {
            health--;
            if (health > 0)
                Debug.Log("Player took dote damage\n" + "Current HP: " + health);
            else
                Debug.Log("Player has died");

            if(health == 0)
            {
                Debug.Log("Player use antidote");
                break;
            }
        }*/
        // 6.2 for
        /*int MAX_COUNT = 10;
        for (int cnt = 0; cnt < MAX_COUNT; cnt++)
        {
            if (health == 100)
            {
                Debug.Log("Maximum health of player is 100.");
                break;
            }
            health += 10;
            Debug.Log("Treated with bandages...\nHealth: " + health);      
        }
        for(int idx = 0; idx < monsters.Length; idx++)
            Debug.Log("All monsters in this area: " + monsters[idx]);
        // 6.3 foreach
        foreach(string monster in monsters)
            Debug.Log("Using ***foreach***\n" +
                "All monsters in this area: " + monster);*/

        // 7.1 Call functions
        Heal();
        for (int idx = 0; idx < monsters.Length; idx++)
        {
            Debug.Log("Battle Result: " + Battle(level, monsterLevel[idx]) +
                " to '" + monsters[idx] + "'");
        }

        // 8. Class
        // 8.1 Instance
        Actor player1 = new Actor();
        player1.ID = 0;
        player1.name = "Ggoong";
        player1.title = "Fancy";
        player1.weaponName = "Hammer";
        player1.strength = 3.3f;
        player1.level = 5;

        Debug.Log(player1.Talk());
        Debug.Log(player1.HasWeapon());
        player1.LevelUp();
        Debug.Log("Player1 ID: " + player1.ID);
        Debug.Log("Player1 Name: " + player1.title + " " + player1.name);
        Debug.Log("Player1 Level: " + player1.level);
        Debug.Log("Player1's Weapon: " + player1.weaponName);
        Debug.Log("Player1 Strength: " + player1.strength);


        // 8.2 Class Inheritance
        Player player2 = new Player();
        player2.ID = 1;
        player2.name = "Tarii";
        player2.title = "Cute";
        player2.weaponName = "Toe Beans";
        player2.strength = 0.5f;
        player2.level = 2;

        Debug.Log("Next Player appears!");
        Debug.Log(player2.Talk());
        Debug.Log(player2.HasWeapon());
        player2.LevelUp();
        Debug.Log("Player2 ID: " + player2.ID);
        Debug.Log("Player2 Name: " + player2.title + " " + player2.name);
        Debug.Log("Player2 Level: " + player2.level);
        Debug.Log("Player2's Weapon: " + player2.weaponName);
        Debug.Log("Player2 Strength: " + player2.strength);
        Debug.Log("Player2 Move: " + player2.Move());
        Debug.Log("Player2 Stop: " + player2.Stop());
        Debug.Log("Player2 Hit: " + player2.Hit());
    }

    // 7. Function(Method)
    void Heal()
    {
        health += 10;
        Debug.Log("Got Healed\nCurrent Health: " + health);
    }

    string Battle(int playerLevel, int enemyLevel)
    {
        string battleResult;
        if (playerLevel >= enemyLevel)
            return battleResult = "Player Win";
        else
            return battleResult = "Player Lost";
    }
}

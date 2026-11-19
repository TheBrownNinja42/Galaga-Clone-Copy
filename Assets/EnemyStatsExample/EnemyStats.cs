using UnityEngine;

//this line adds this scriptable object into the right click create menu in the project window allowing us to create instances in unity itself
[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    // The enemy's name
    [SerializeField] private string enemyName = "Enemy";
    // How much health it starts with
    [SerializeField] private int health = 10;
    // How fast it moves
    [SerializeField] private float moveSpeed = 3f;
    // Points for destroying it
    [SerializeField] private int points = 100;
    // The colour to draw it
    [SerializeField] private Color colour = Color.white;

    // Lets other scripts read the colour
    public Color GetColour()
    {
        // Hand back the colour
        return colour;
    }

    // Puts every value in one line of text, for the Console
    public string Describe()
    {
        // Join the values with labels
        return enemyName + ": " + health + " health, speed " + moveSpeed + ", worth " + points + " points";
    }
}

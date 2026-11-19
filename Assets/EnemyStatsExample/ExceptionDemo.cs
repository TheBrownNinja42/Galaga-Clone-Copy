// Gives access to MonoBehaviour and Debug
using UnityEngine;

// Shows what try/catch does: turns typed text into a number without crashing
public class ExceptionDemo : MonoBehaviour
{
    // Pretend the player typed this into an age box
    [SerializeField] private string typedAge = "abc";

    // Runs once when the scene starts
    private void Start()
    {
        // try: run this code, but if anything in it crashes, jump to catch
        try
        {
            // int.Parse turns text like "16" into the number 16; "abc" makes it crash
            int age = int.Parse(typedAge);
            // Only reached if Parse worked
            Debug.Log("Next year you'll be " + (age + 1));
        }
        // catch: only runs if something in try crashed; e holds what went wrong
        catch (System.Exception e)
        {
            // A yellow warning instead of a red crash
            Debug.LogWarning("That's not a number: " + typedAge + " (" + e.Message + ")");
        }

        // Proves the script kept going after the crash was caught
        Debug.Log("Start carried on");
    }
}
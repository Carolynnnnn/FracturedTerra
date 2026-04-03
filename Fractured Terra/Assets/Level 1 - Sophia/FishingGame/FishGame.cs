using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishGame : MonoBehaviour, IInteractable // Simon says-like game for catching fish
{
    public PlayerController playerController; // Helps pause movement
    public Canvas gameCanvas; // Canvas on which the game appears
    public Image up,  down, left, right; // Greyed out buttons, disappear briefly to show colored version underneath
    public InventoryManager inventoryManager; // Keeps track of player's inventory
    public Sprite seaBass, clownFish; // Prize sprites
    
    private bool isGameActive = false; // Keeps track of game status
    private int[] computerTurn = new int[4]; // Keeps track of which button player has to press to win
    private int currentInputIndex = 0; // How many buttons player has pressed
    
    public bool CanInteract() // Game can be started when no other games are active
    {
        return !isGameActive;
    }

    public void Interact()
    {
        isGameActive = true;
        playerController.CanMove = false; // Pause player movement
        gameCanvas.gameObject.SetActive(true); // Shows game canvas
        
        for (var i = 0; i < 4; i++) // Generate input order
        {
            computerTurn[i] = UnityEngine.Random.Range(1, 5); // Generates an int from 1 to 4
            // up = 1, down = 2, left = 3, right = 4
        }
        StartCoroutine(ComputerTurn()); // Computer shows player what to press
    }

    private IEnumerator ComputerTurn()
    {
        for (var i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.3f); // Wait half a second before starting
            switch (computerTurn[i])
            {
                case 1:
                    up.gameObject.SetActive(false); // Hide greyed-out button
                    yield return new WaitForSeconds(0.5f); // Wait another half second
                    up.gameObject.SetActive(true); // Show greyed-out button
                    break;
                case 2:
                    down.gameObject.SetActive(false);
                    yield return new WaitForSeconds(0.5f);
                    down.gameObject.SetActive(true);
                    break;
                case 3:
                    left.gameObject.SetActive(false);
                    yield return new WaitForSeconds(0.5f);
                    left.gameObject.SetActive(true);
                    break;
                case 4:
                    right.gameObject.SetActive(false);
                    yield return new WaitForSeconds(0.5f);
                    right.gameObject.SetActive(true);
                    break;
            }
        }
        StartCoroutine(PlayerTurn()); // Start player turn
    }

    private IEnumerator PlayerTurn()
    {
        currentInputIndex = 0; // Reset
        while (currentInputIndex < 4)
        {
            // Listen for key presses
            if (Input.GetKeyDown(KeyCode.UpArrow)) CheckInput(1);
            if (Input.GetKeyDown(KeyCode.DownArrow)) CheckInput(2);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) CheckInput(3);
            if (Input.GetKeyDown(KeyCode.RightArrow)) CheckInput(4);
            
            yield return null; // Wait for next frame
        }
        EndGame(true); // End the game as a win
    }

    private void CheckInput(int input)
    {
        if (!isGameActive) return; // Verify game is still active
        StartCoroutine(ShowInput(input)); // Show the player what they inputted on the screen
        
        if (input == computerTurn[currentInputIndex]) // If the input is correct
        {
            currentInputIndex++;
        }
        else
        {
            EndGame(false); // End the game as a loss
        }
    }

    private IEnumerator ShowInput(int value)
    {
        switch (value)
        {
            case 1:
                up.gameObject.SetActive(false); // Hide greyed-out button
                yield return new WaitForSeconds(0.2f); // Wait a bit
                up.gameObject.SetActive(true); // Show greyed-out button
                break;
            case 2:
                down.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                down.gameObject.SetActive(true);
                break;
            case 3:
                left.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                left.gameObject.SetActive(true);
                break;
            case 4:
                right.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                right.gameObject.SetActive(true);
                break;
        }
    }
    
    private void EndGame(bool won)
    {
        StopAllCoroutines();

        // Reset to normal gameplay settings
        gameCanvas.gameObject.SetActive(false);
        playerController.CanMove = true;
        isGameActive = false;
        
        // Reset buttons to prevent errors on replay
        up.gameObject.SetActive(true);
        down.gameObject.SetActive(true);
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);

        if (won)
        {
            int prizeWon = UnityEngine.Random.Range(1, 5); // Chooses which fish is won
            Debug.Log("Caught fish!");
            
            InventoryItem prize;
            if (prizeWon == 4) // 25% chance of getting a clownfish
            {
                prize = new InventoryItem(
                    "Clownfish", 
                    "This traffic cone-colored fish is supposedly very comedic.", 
                    clownFish, 1, false, null);
            }
            else
            {
                prize = new InventoryItem(
                    "Sea Bass", 
                    "A frustratingly common fish. Slimy and green.", 
                    seaBass, 1, false, null);
            }
            inventoryManager.AddItem(prize); // Adds item to inventory
        }
        else
        {
            Debug.Log("Fish escaped!");
        }
    }
}

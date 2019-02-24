/*
1-
    In the method AddMonster in the Fight class, the code creates a monster, set its properties and adds it to the list of available monsters for the hero to fight. 
    This might not be a big deal now but it opens the room for errors.
    A developer could potentially create a monster without Name, Strength, Defense and OriginalHP.
    Your first task is to refactor the code to stop this problem. 
    In other words, make it required that Name, Strength, Defense, OriginalHP, and CurrentHP parameters are provided when creating monsters across our code base.

2-	
    It looks like the methods HeroTurn, MonsterTurn and Win in the Fight class are all passing around a reference to the Monster that is being fought. 
    It doesn’t look that bad but we have some repeated code.
    It also opens the window for a developer to pass a different monster reference as a parameter while a fight is happening. 
    Your second task is to refactor your code in order to remove the repetition and avoid the described problem.

3-
    The monsters in our application should have a difficulty level assigned to them. 
    Your third task is to add a new functionality to the Monster class that will allow us to tell if the monster difficulty level is Easy, Medium or Hard. 
    Make sure you also refactor the code in order to prevent developers from creating monsters without assigning a difficulty level. Tip: Try avoiding “magic strings”.

4-
    Our game is pretty boring right now. 
    There is only one monster the hero can fight. 
    Your fourth task is to make the game not so boring by adding at least 35 monster that the hero can fight. 
    Remember to take into consideration the difficulty of the monster when deciding about their HP, Strength and Defense. 


5-
    Your final task is to make the game more interesting by allowing the hero to fight only a range of 5 monsters per day based on the weekday. 
    A monster should be selected randomly every time a new fight starts.
*/
namespace OOP_RPG
{
    public class RPG
    {
        public static void Main()
        {
            // A new object named "game" is being instantiated from our Game Class. game is an instance of the Game Class. 
            // game is also a variable that is pointing to the instance of that Class
            Game game = new Game();
            game.Start();
        }
    }
}
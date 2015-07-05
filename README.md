# Underworld2171
Progress on a small game I am creating.

Basically, the project is a partial conversion of the popular board game "City of Remnants". 
The player can choose from a series of actions to take each turn, such as occupying tiles, 
recruiting units, or selling and buying goods. A player loses when their headquarters, or corner tile
is taken by another tile. So far, the game only supports one action, which is buying and developing tiles. 
Since the functionality for units would be needed before players can actually take tiles, the game currently
has no definite end or playability but there is a sufficient framework in place to provide this soon. 

So far there are a number of internal classes and helper classes to assist the GUI in performing the game. There is 
a game state object which controls the board, squares, players, and so on, as well classes which perform functions such 
as advancing the turn, processing the overall game loop, and firing events for various buttons. I'm developing this in 
Visual Studio using c#, and in a windows form application, so it appears a bit clunky. My main task at the moment has been
dealing with separation of concerns and organizing the code before it gets big enough that an anti-pattern could make it
impossible to look at or work with.

I hope to have a working game in the near future! 

- July 5th, 2015

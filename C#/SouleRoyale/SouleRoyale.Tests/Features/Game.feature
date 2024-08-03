Feature: Game Features
	Features to manage the behavior of game

Scenario: Create a game
	Given I ask to create a new game
	When I create a new game
	Then 2 teams have been created
	And Teams have 11 players
	And The turn number is 0
	And The max turn number is 7
	And The soule is in 0 position
	And The game is not over
	And Winner is null

Scenario: Set players of first team intial positions
	Given a new game
	When captain of first team gives the following instructions 2 1 1 1 1 2 1 1 1 1 3
	Then players are in -2 -1 -1 -1 -1 -2 -1 -1 -1 -1 -3 lines

Scenario: Set players of first team invalid intial positions
	Given captain of first team gives the following instructions a invalid operation exception fires:
		| Instructions           |
		| 0 0 1 1 1 2 1 1 1 1 2  |
		| 4 1 4 1 1 2 1 1 1 1 2  |
		| -1 1 1 1 1 2 1 1 1 1 2 |

Scenario: A game winned by first team at thrid round
	Given a new game
	When captains give the following instructions:
		| Equipe 1                 | Equipe 2              |
		| 2 1 1 1 1 2 1 1 1 1 2    | 1 1 1 1 2 1 1 2 2 2 1 |
		| 0 - 0 0 0 + + + + + -    | + + + + 0 + + 0 + + + |
		| 0 + 1 + 1 + + 11 11 + +  | + 7 7 + + 7 7 + + + 8 |
		| + + 1 + 1 11 X 11 11 + 0 | - 9 9 - - 9 9 8 8 8 8 |
	Then The game is over on round 3
	And The first Team is the winner
	And The number 7 of first team is ko

Scenario: A game wihtout winner at the end of 7th round
	Given a new game
	When captains give the following instructions:
	| Equipe 1                 | Equipe 2                     |
	| 1 1 1 2 2 2 2 3 3 3 3    | 1 1 1 2 2 1 1 1 2 2 2        |
	| + + + + + 0 0 + + 0 +    | + + 0 - 0 + + 0 0 0 0        |
	| + 2 1 + + + + 0 - 0 +    | 3 3 1 + - 4 4 1 + 0 -        |
	| - 6 6 6 6 1 1 0 0 0 0    | + + + - + + 4 1 + 0 +        |
	| 9 9 9 9 - 1 7 1 0 + -    | 7 7 4 + + X 3 + + + +        |
	| 8 8 8 X 9 9 9 - + + +    | X 7 3 + + X - + + + +        |
	| 10 10 X X 8 2 7 + + + 10 | X 7 + + 0 X + + X + 0        |
	| + 4 X X + + X + + + +    | X 11 11 10 10 X 10 X X 11 11 |
	Then The game is over on round 7
	And No winner

Scenario: A game winned by second team at round
	Given a new game
	When captains give the following instructions:
	| Equipe 1                  | Equipe 2              |
	| 1 1 1 2 2 1 1 2 1 2 1     | 2 1 1 1 3 2 1 1 2 2 2 |
	| + + + 0 + + + 0 0 + +     | 0 0 0 + 0 0 0 + 0 + + |
	| 4 4 4 + + + 8 + + 4 7     | 0 0 + + + + + + + 6 + |
	| 7 7 7 8 11 - 11 8 11 8 11 | + + + X + + + + + + + |
	Then The game is over on round 3
	And The number 4 of second team is ko
	And The number 1 of first team is in line 0
	And The second Team is the winner
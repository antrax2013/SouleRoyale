Feature: Game Features
	Features to manage the behavior of game

Scenario: Create a game
	Given I ask to create a new game
	When I create a new game
	Then 2 teams have been created
	And Teams have 11 players
	And The first team is active
	And The turn number is 0
	And The max turn number is 7

Scenario: Set players of first team intial positions
	Given a new game
	When captain of first team gives the following instructions 2 1 1 1 1 2 1 1 1 1 3
	Then playes are in 2 1 1 1 1 2 1 1 1 1 3 lines

Scenario: Set players of first team invalide intial positions
	Given captain of first team gives the following instructions a invalid operation exception fires:
		| Instructions |
		| 0 0 1 1 1 2 1 1 1 1 2 |
		| 4 1 4 1 1 2 1 1 1 1 2 |
		| -1 1 1 1 1 2 1 1 1 1 2 |
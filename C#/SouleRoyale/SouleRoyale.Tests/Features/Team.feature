Feature: Team Features
	Features to manage the behavior of soule team

Scenario: Create a team
	Given I ask to create a new team
	When I create a new team
	Then The team have 11 players
	And All players have 4 life points

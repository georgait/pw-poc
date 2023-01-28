Feature: AssertBookReferencesAndChooceOne

A short summary of the feature

@tag1
Scenario: [scenario name]
	Given the user "testUser" navigates to "baseUrl"
	When he types the username and password
	Then he chooses the "Learning JavaScript Design Patterns" book
	And he navigates to website of the book
	Then he selects the last reference
	And he confirms that the page not found
	Then he closes the window
	And he clicks on "Back to Book Store"
	Then he logs out

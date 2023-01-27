Feature: Login

A short summary of the feature

@tag1
Scenario: Login to demoqa.com
	Given the user "testUser" navigates to "baseUrl"
	When he types the username and password
	#Then he should see 2 books
	Then he logs out
	Then he should see the "Login" title

Feature: AccessSO

A short summary of the feature

@tag1
Scenario: Access stackoverflow.com
	Given navigate to target url
	When accept all cookies
	Then go to questions
	And select bounty tab
	Then select the first post
	

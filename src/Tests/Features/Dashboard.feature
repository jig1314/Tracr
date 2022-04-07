Feature: Dashboard

As a user, I need to be able to see a home page so that I can view dashboards related to my properties.

@BDD
Scenario: Existing user logging in and viewing dashboard
	Given I have registered for an account
		| FirstName | LastName | Email             | Username | Password  | ConfirmPassword | Gender | CountryofResidence | DateofBirth |
		| Test      | Name     | testing@gmail.com | testName | NotR3@Lpw | NotR3@Lpw       | Male   | United States      | 01/01/2000  |
	When I click the log in link
	And I log in
		| Username | Password  |
		| testName | NotR3@Lpw |
	Then I have the ability to see the dashboard

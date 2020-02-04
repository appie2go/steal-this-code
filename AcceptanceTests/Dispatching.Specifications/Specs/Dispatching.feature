Feature: Rides
	In order to save money
	As a dispatching business
	I want to dispatch cabs automatically

@EndToEnd
Scenario: Cab drives customer to the trainstation
	Given a cab
	  And traffic information
	  And a train station called "Utrecht Centraal"
	  And a customer who wants to go to Utrecht Centraal
	 When the customer has been driven to the trainstation
	 Then the cab it's new location is the trainstation
	  And the ride has been registered

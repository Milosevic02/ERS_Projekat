# Smart Thermoregulator Project

## Project Description

The "Smart Thermoregulator" project simulates the operation of a smart temperature control device in indoor spaces. The system consists of a central heater, a temperature regulator, and multiple temperature-sensing devices.

## System Entities

### Device

- Possesses a unique ID.
- Periodically reads the temperature every 3 minutes and sends the temperature value to the regulator.
- A minimum of 4 devices must be registered.

### Regulator

- Provides a user interface (UI) for adjusting the regulator's operating modes and temperatures for each mode.
- The regulator has two operating modes: daytime and nighttime.
- The user inputs the range of hours representing the daytime mode and the desired temperatures for day and night.
- Based on the entered data, the regulator initiates its operation.
- Receives temperature values from devices, stores them, and regulates based on the average temperature and the current mode's temperature.
- If necessary, issues commands to the central heater.
- Logs all events in a text file.
- Only one regulator exists.

### Heater

- Receives on/off commands from the regulator.
- Optimally utilizes resources to maintain the desired temperature.
- Records the amount of time spent being on in the database.
- Stores information about the start time of operation, the quantity of resources consumed during operation.
- Only one central heater exists.

## Configurations

- All times in the system are configurable.

## Implementation and Testing

The project is implemented following SOLID principles and includes unit tests for each system component to ensure reliability and functionality.

## Instructions for Running

1. Clone the repository to your local machine.
2. Configure settings as needed.
3. Run the application.

## Authors

- Dragan Milosevic (@Milosevic02)
- Vladislav Petkovic (@SaladVlad)

## Acknowledgements

Special thanks to our project supervisor, Teodora Ruvceski (@teodoraruvceski) , for providing guidance and support throughout the development of this project. Their valuable input and assistance significantly contributed to its success.


## License

This project is available under the [License](link-to-license).

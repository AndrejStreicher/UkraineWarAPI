<br/>
<p align="center">
  <h3 align="center">UkraineWarAPI</h3>

  <p align="center">
    Simple ASP.NET Web API that provides you with the latest verified information about the Russia-Ukraine war from trusted sources.
    <br/>
    <a href="http://api.streicher.tech/ukrainewar/" title="UkraineWarApi">View Demo</a>
    <br/>
  </p>
</p>

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Table Of Contents

* [About the Project](#about-the-project)
* [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Installation](#installation)
* [Usage](#usage)
* [Contributing](#contributing)
* [License](#license)
* [Authors](#authors)
* [Acknowledgements](#acknowledgements)

## About The Project

This project is dedicated to providing up-to-date information on the ongoing war in Ukraine. It compiles information from multiple trusted sources to give factual information on the conflict.

## Built With

ASP.NET Web API  
xUnit

## Getting Started

While this app can be compiled from source, I believe the easiest way to get it up and running is to use a Docker container.

### 🛠️ Installation
```
1. git clone https://github.com/AndrejStreicher/UkraineWarAPI
2. cd UkraineWarAPI
3. docker build -t ukraine-war-api .
4. docker run -d -p your-preferred-port:8080 ukraine-war-api
```
## Usage

You can use this API as a light-weight backend for your news app or simply a way to get the latest news and daily summaries of the conflict straight in your browser. You can try out the api on this link: 

api.streicher.tech/ukrainewar/[ENDPOINT]

Available endpoints:
  - latesttakeaways: Provides the latest key takeaways of the Russia-Ukraine war.
  - keytakeaways/{dateTimeString}: Provides the key takeaways of the input date.
## 💻 Built with
ASP.NET Web API
## 🛡️ License

Distributed under the MIT License. See [LICENSE](https://github.com/AndrejStreicher/UkraineWarAPI/blob/main/LICENSE.md) for more information.

## Authors

* [Andrej Streicher](https://www.linkedin.com/in/andrej-streicher-35658027b/) - *Comp Sci Student* - 

## Acknowledgements
### Sources:
* [Institute for the Study of War](https://www.understandingwar.org/)

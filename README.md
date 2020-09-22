# http2adls
This code performs a http(s) reqeust and stores the data in an Azure Storage container. From there it can be picked up for further analysis.

## Microservice
The code is build to be run as a microservice inside a container. There are several options to pass in a configuration:
- Create a new container based on this one with a config.json file in the same folder as the binaries.
- Map a volume as local folder and use an environment variable to point to the configuration file.
- Pass options as environment variables.

## Configuration file
The configuration file is a json file where you can set several variables.
``` json
{
    "web": {
        
    },
    "storage": {
        "connectionString": "<Azure storage connectionstring>"
    }
}
```

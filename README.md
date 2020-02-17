# Swashbuckle.FlattenAllOf.Extension
Flatten allOf collections with a single item in to a traditional reference

![Publish to nuget](https://github.com/waxtell/Swashbuckle.FlattenAllOf.Extension/workflows/Publish%20to%20nuget/badge.svg)

While syntactically correct, I find the "allOf" below redundant.  The "region" property can only ever be an instance of Region.

```
      "WeatherForecast": {
        "type": "object",
        "properties": {
          "date": {
            "type": "string",
            "format": "date-time"
          },
          "temperatureC": {
            "type": "integer",
            "format": "int32"
          },
          "region": {
            "allOf": [
              {
                "$ref": "#/components/schemas/Region"
              }
            ],
            "nullable": true
          }
        },
```

The FlattenAllOf extension replaces the "allOf" with a traditional reference as such
```
      "WeatherForecast": {
        "type": "object",
        "properties": {
          "date": {
            "type": "string",
            "format": "date-time"
          },
          "temperatureC": {
            "type": "integer",
            "format": "int32"
          },
          "region": {
            "$ref": "#/components/schemas/Region"
          }
        },
```

Startup.cs
```csharp
            app.UseSwagger
            (
                options => options.FlattenAllOfs()
            );

```

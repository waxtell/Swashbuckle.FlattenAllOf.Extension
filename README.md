# Swashbuckle.FlattenAllOf.Extension
Flatten allOf collections with a single item in to a traditional reference

While syntactically correct, I find the "allOf" below redundant.  The "nested" property can only ever be an instance of NestedObject.

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
          "nested": {
            "allOf": [
              {
                "$ref": "#/components/schemas/NestedObject"
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

# Vuforia CLI App

## Overview

The Vuforia CLI App is a command-line interface tool for interacting with the Vuforia platform. It allows users to manage targets, retrieve data, and perform various operations on the Vuforia database using their access and secret keys.

## Features

- List all targets in the database
- Retrieve details for a specific target
- Insert new targets
- Update existing targets
- Delete targets
- Check for similar targets
- Generate summary reports
- Get a summary of the database

## Prerequisites

- .NET SDK (version 8.0 or higher)
- Vuforia Access Key and Secret Key

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/gachris/VuforiaWebService.git
   cd VuforiaWebService
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Publish the application:

   ```bash
   dotnet publish -c Release -o ./publish
   ```

Alternatively, you can download the pre-built executable from the [GitHub Releases](https://github.com/gachris/VuforiaWebService/releases) page.

## Usage

To use the Vuforia CLI app, you must provide your access key and secret key as command-line arguments. Here are some examples:

### List All Targets

```bash
vuforia --access-key <your_access_key> --secret-key <your_secret_key> list
```

### Get Target Details

```bash
vuforia --access-key <your_access_key> --secret-key <your_secret_key> get --target-id <target_id>
```

### Insert a New Target

```bash
vuforia --access-key <your_access_key> --secret-key <your_secret_key> insert --target-name "SampleTarget" --target-width 1 --target-image "<path_to_your_image>" --target-active-flag true --target-metadata "<your_target_metadata>"
```

**Parameters:**
- `<your_access_key>`: Your Vuforia access key.
- `<your_secret_key>`: Your Vuforia secret key.
- `"SampleTarget"`: The name you want to give to the target.
- `1`: The width of the target in the appropriate units (meters, centimeters, etc.).
- `<path_to_your_image>`: The full path to the image file you want to use for the target. Ensure that the image is accessible by the application.
- `true`: Flag to indicate whether the target is active.
- `<your_target_metadata>`: Metadata associated with the target. This can be any string that provides additional context about the target.

### Example

```bash
vuforia --access-key 0bb705edc1e10c7cf205da5b63fbf5285996443d --secret-key 142526decc8940c7a9d5ece4198f47d748b9b132 insert --target-name "SampleTarget" --target-width 1 --target-image "C:\Users\YourUsername\Pictures\sample_image.jpg" --target-active-flag true --target-metadata "This is a sample target for demonstration."
```

### Update an Existing Target

```bash
vuforia --access-key <your_access_key> --secret-key <your_secret_key> update --target-id <target_id> --target-name "UpdatedTarget" --target-width 1
```

### Delete a Target

```bash
vuforia --access-key <your_access_key> --secret-key <your_secret_key> delete --target-id <target_id>
```

### Check for Similar Targets

```bash
vuforia --access-key <your_access_key> --secret-key <your_secret_key> check-similar --target-id <target_id>
```

### Generate a Summary Report

```bash
vuforia --access-key <your_access_key> --secret-key <your_secret_key> summary-report --target-id <target_id>
```

### Get Database Summary

```bash
vuforia --access-key <your_access_key> --secret-key <your_secret_key> database-summary
```

## Notes

- Replace `<your_access_key>`, `<your_secret_key>`, and `<target_id>` with your actual Vuforia credentials and target IDs.
- The application can also be run using the `dotnet run` command, for example:

```bash
dotnet run --access-key <your_access_key> --secret-key <your_secret_key> list
```

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Contributing

If you would like to contribute to this project, please fork the repository and submit a pull request.

## Acknowledgements

- [Vuforia](https://developer.vuforia.com/) for providing the AR platform.

# Username Generator

## About the Project

This repository contains a set of user interfaces to randomly generate customized usernames. These usernames are collated in a title-case format (e.g. "WordWord") with greater customization such as character count and number of syllables per word.

> **Note for using filters**: Filtering by syllable counts may occasionally be off by one, as English is a language that often doesn't play by a consistent set of phonetic rules. This makes it tricky to detect the number of syllables using an algorithm.

### Applications

#### Console

<img alt="Console app screenshot" src="https://github.com/gridlocdev/UsernameGenerator/assets/60323737/211b0c48-ab4c-42f2-b10a-ecbc0cc850b7" width=75% />

#### Terminal UI

<img alt="Terminal UI app screenshot" src="https://github.com/gridlocdev/UsernameGenerator/assets/60323737/020f5dfd-2796-490c-beb6-94ee928c5bb4" width=75% />

#### Web

<img alt="Web app screenshot" src="https://github.com/gridlocdev/UsernameGenerator/assets/60323737/2b4779e6-7a47-483d-a0df-bd60ac6bf9bf" width=75% />

#### Desktop

<img alt="Desktop app screenshot" src="https://github.com/gridlocdev/UsernameGenerator/assets/60323737/3c2b4e8d-b032-47f5-81c5-ab95cbe4b4c7" width=75% />

## Getting Started

### Prerequisites

- .NET 8 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

### Installation

1. Clone the repo:
    ```
    git clone https://github.com/gridlocdev/UsernameGenerator.git
    ```
2. Open the directory of the app you would like to run (e.g. UsernameGenerator.Console)
    ```
    cd UsernameGenerator.Console
    ```
3. Run the application
    ```
    dotnet run
    ```

### Using your own word list

1. Create a fork of the project
2. In the `UsernameGenerator.Core` directory, edit the file named `./Data.words.txt` to include your own word list
3. Create a new Console app project
4. In the `csproj` file, add a reference to the `UsernameGenerator.Core` project
    ```XML
    <ItemGroup>
      <ProjectReference Include="..\UsernameGenerator.Core\UsernameGenerator.Core.csproj" />
    </ItemGroup>
    ```
5. Replace the contents of `Program.cs` with the following
    ```C#
    using UsernameGenerator.Core;

    SyllableWriter.WriteToJson();
    ```
6. Run the new Console app
    ```
    dotnet run
    ```

## License

Distributed under the GPL-3.0. See the `LICENSE` file for more information.

## Acknowledgements

- [Online Plain Text English Dictionary (OPTED)](https://www.mso.anu.edu.au/~ralph/OPTED/)
- [EddyDN's OPTED export](https://github.com/eddydn/DictionaryDatabase)

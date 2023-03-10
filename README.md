# Username Generator
## Overview

This repository contains a set of user interfaces to randomly generate customized usernames. These usernames are collated in a title-case format (e.g. "WordWord") with greater customization such as character count and number of syllables per word.

The default word data was sourced from [EddyDN's JSON version](https://github.com/eddydn/DictionaryDatabase) of the public domain [Online Plain Text English Dictionary](https://www.mso.anu.edu.au/~ralph/OPTED/).

## Solution Layout

There are currently four types of available user interfaces: a basic Console app, a Terminal UI, a local web UI, and a Desktop app. All four of these share and reuse the same core library.

```mermaid
  flowchart
    Core["Core Library"]
    Console["Console App"]
    TUI["Terminal UI"]
    Web["Web App"]
    Desktop["Desktop App"]

    Desktop & Console & Web & TUI --> Core
```

### Console

<img src="https://user-images.githubusercontent.com/60323737/215591931-f534fb33-ea6d-4ea5-a5d0-3bab9b1abca2.png" width=75% />
 
### Terminal UI

<img src="https://user-images.githubusercontent.com/60323737/215607741-abba7ad5-7f42-4a2f-9208-7addfcfe74c9.png" width=75% />

### Web

<img src="https://user-images.githubusercontent.com/60323737/215607216-4d3215cb-83af-457b-9691-90a99c4b99b9.png" width=75% />

### Desktop

<img src="https://user-images.githubusercontent.com/60323737/215608150-c66e6bcb-ee67-4b00-a54f-4b6bbe17beea.png" width=75% />

## Usage

1. Install the .NET 6 SDK
2. Clone the repository locally
3. Navigate to the cloned folder in the filesystem and open the project UI you would like to run (Basic Console, Terminal UI, Local Web, Desktop)
3. Run `dotnet run` in a terminal

## Using your own Data

If you'd like to use your own word list, the core library also includes a class titled `SyllableWriter` which contains a pair of methods that can be used to generate syllable counts from an existing word list.

## Known Bugs

- English is a language that often doesn't play by a consistent set of phonetic rules, and as a result the count of syllables detected may be occasionally off by one based on the word endings.

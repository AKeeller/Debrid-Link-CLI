# Debrid-Link CLI

![Static Badge](https://img.shields.io/badge/AKeeller-Debrid--Link%20CLI-blue)
[![Build Debrid-Link CLI](https://github.com/AKeeller/Debrid-Link-CLI/actions/workflows/build.yml/badge.svg)](https://github.com/AKeeller/Debrid-Link-CLI/actions/workflows/build.yml)

A cross-platform command-line client for **Debrid-Link**, enabling torrent management, account inspection, and file downloads with rich progress visualization.

## Features

- Fetch **Debrid-Link** account information.
- List, select, and download torrents interactively.
- Cross-platform single-file executable builds.
- Rich terminal UI using [Spectre.Console](https://spectreconsole.net).

## Installation

Download the latest version from [GitHub Releases](https://github.com/AKeeller/Debrid-Link-CLI/releases/latest). The following binaries are available:

| OS      | Architectures   |
|---------|-----------------|
| Linux   | ARM, ARM64, x64 |
| macOS   | ARM64, x64      |
| Windows | ARM64, x64      |

## Authentication

All commands require an API key.  
The CLI automatically tries the following sources **in order**:

1. **Environment variable**
2. **Configuration file**

If no API key is found, the CLI will display instructions.

## Root Commands

### `account`

Display Debrid-Link account information.

#### Description

Displays user info, account type, points, and premium status

#### Usage

```sh
dlcli account
```

### `downloader`

List downloader links and interactively download files.

#### Description

- List downloader links
- Allows interactive multi-selection
- Download or delete more files at once

#### Usage

```sh
dlcli downloader
```

### `torrent` (alias: `seedbox`)

List torrents and interactively download files from your seedbox.

#### Description

- Lists active torrents
- Allows interactive selection
- Downloads all files from the selected torrent
- Delete torrents

#### Usage

```sh
dlcli torrent
```

or

```sh
dlcli seedbox
```

## Downloader Subcommands

### `downloader limits` (alias: `usage`)

Show downloader usage and limits.

#### Description

- Displays current usage percentages
- Shows reset timers and quota limits

#### Usage

```sh
dlcli downloader limits
```

or

```sh
dlcli downloader usage
```

## Torrent Subcommands

### `torrent add`

Add a torrent to Debrid-Link.

#### Description

Accepts either a magnet / URL or a local `.torrent` file

#### Options

| Option   | Alias | Description                |
|----------|-------|----------------------------|
| `--link` | `-l`  | Magnet link or torrent URL |
| `--file` | `-f`  | Path to a `.torrent` file  |

#### Examples

```sh
dlcli torrent add --link magnet:?xt=urn:btih:...
```

```sh
dlcli torrent add --file ./example.torrent
```

### `torrent limits` (alias: `usage`)

Show seedbox usage and limits.

#### Description

- Displays current usage percentages
- Shows reset timers and quota limits
- Includes active transfers and monthly statistics

#### Usage

```sh
dlcli torrent limits
```

or

```sh
dlcli torrent usage
```

## TL;DR

```text
dlcli
├── account
├── downloader
│   └── limits (alias: usage)
└── torrent (alias: seedbox)
    ├── add
    └── limits (alias: usage)
```

## Exit Codes

- `0` — Success
- `1` — Error (missing API key, API failure, invalid input, etc.)

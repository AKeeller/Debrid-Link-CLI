# Debrid-Link CLI

![Static Badge](https://img.shields.io/badge/AKeeller-Debrid--Link%20CLI-blue)
[![Build Debrid-Link CLI](https://github.com/AKeeller/Debrid-Link-CLI/actions/workflows/build.yml/badge.svg)](https://github.com/AKeeller/Debrid-Link-CLI/actions/workflows/build.yml)

A cross-platform command-line client for [Debrid-Link](https://debrid-link.com), enabling torrent management, account inspection, and file downloads with rich progress visualization.

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

## Commands

| Command      | Alias     | Action                       |
|--------------|-----------|------------------------------|
| `account`    | —         | Show account info            |
| `downloader` | —         | List/manage downloader links |
| `torrent`    | `seedbox` | List/manage torrents         |

## `downloader` Subcommands

| Command             | Alias   | Action                     |
|---------------------|---------|----------------------------|
| `limits`            | `usage` | Show usage, quotas, resets |

## `torrent` Subcommands

| Command          | Alias   | Action                               | Options                                 |
|------------------|---------|--------------------------------------|-----------------------------------------|
| `add`            | —       | Add torrent via magnet or `.torrent` | `--link`, `--file`, `--links-from-file` |
| `limits`         | `usage` | Show usage, active transfers, stats  | —                                       |

## Examples

```sh
dlcli account
```

```sh
dlcli downloader
```

```sh
dlcli downloader limits
```

```sh
dlcli torrent add --link magnet:?xt=urn:btih:...
```

```sh
dlcli torrent add --file ./example.torrent
```

```sh
dlcli torrent add --links-from-file list.txt
```

```sh
dlcli torrent limits
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

## Contributing

Issues and pull requests are welcome.  
Please keep changes focused and consistent with the existing style.

## Disclaimer

> **Disclaimer / Non-affiliation**
>
> This project is a **personal, unofficial, home project**.
>
> I am **not affiliated with, endorsed by, sponsored by, or otherwise connected to Debrid-Link** in any way.
>
> This CLI is simply a third-party client that interacts with the publicly available [Debrid-Link API](https://debrid-link.com/api_doc).  
> All trademarks, service names, and logos belong to their respective owners.

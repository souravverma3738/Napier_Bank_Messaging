# Napier Bank Messaging System

A Windows desktop application built with C# and WPF (Windows Presentation Foundation) for processing and managing various types of messages including 
SMS, Email, and Twitter messages. The system is designed to help Napier Bank efficiently handle and categorize incoming communications.

## Features

### Message Processing
- **SMS Messages** - Process and validate SMS communications
- **Email Messages** - Handle standard emails and Significant Incident Reports (SIR)
- **Twitter Messages** - Process tweets with hashtag and mention extraction

### Key Functionalities
- **Hashtag Tracking** - Automatically extracts and lists trending hashtags from Twitter messages
- **Mention Tracking** - Identifies and tracks @mentions in Twitter messages
- **SIR List** - Maintains a list of Significant Incident Reports from emails
- **URL Quarantine** - Detects URLs in messages and quarantines them for security purposes
- **Text Speak Expansion** - Expands common text abbreviations using a CSV dictionary

## Architecture

The application follows a layered architecture pattern:

```
Napier_Bank/
├── Business Layer/          # Business logic and message processing
│   ├── email.cs            # Email message handling
│   ├── sms.cs              # SMS message handling
│   ├── Twitter.cs          # Twitter message handling
│   ├── Hashtag.cs          # Hashtag extraction logic
│   ├── Mention.cs          # Mention extraction logic
│   ├── Sir.cs              # Significant Incident Report handling
│   └── URL1.cs             # URL detection and quarantine
│
├── Data Layer/              # Data management and storage
│   ├── List.cs             # General list management
│   ├── List_email.cs       # Email list management
│   ├── Last1.cs            # Recent messages tracking
│   └── textwords.csv       # Text abbreviation dictionary
│
├── Properties/              # Application properties
├── class diagram/           # UML class diagrams
│
├── MainWindow.xaml          # Main application window UI
├── MainWindow.xaml.cs       # Main window code-behind
├── HashtagList.xaml         # Hashtag list view
├── MentionList.xaml         # Mention list view
├── SIRList.xaml             # SIR list view
├── URL.xaml                 # Quarantined URLs view
├── App.xaml                 # Application configuration
└── App.config               # Application settings
```

## Prerequisites

- **Windows OS** (Windows 10 or later recommended)
- **.NET Framework 4.7.2** or higher
- **Visual Studio 2019/2022** (for development)
- 
## Usage

### Processing Messages

1. **Launch the application** - The main window will appear
2. **Enter message details** - Input the message header and body
3. **Submit for processing** - The system will automatically:
   - Detect message type (SMS/Email/Twitter)
   - Extract relevant information (hashtags, mentions, URLs)
   - Categorize and store the message

### Viewing Reports

- **Hashtag List** - View trending hashtags with occurrence counts
- **Mention List** - View all extracted @mentions
- **SIR List** - View all Significant Incident Reports
- **URL Quarantine** - View quarantined URLs from messages

## Message Types

### SMS (Header starts with 'S')
- Format: `S + 9 digits` (e.g., S123456789)
- Maximum 140 characters
- Text abbreviations are automatically expanded

### Email (Header starts with 'E')
- Format: `E + 9 digits` (e.g., E123456789)
- Maximum 1028 characters
- Standard emails and SIR emails supported
- URLs are detected and quarantined

### Twitter (Header starts with 'T')
- Format: `T + 9 digits` (e.g., T123456789)
- Maximum 140 characters
- Hashtags and mentions are extracted
- Text abbreviations are expanded

## Testing

The project includes a testing folder with unit tests:

```bash
/testing/
```

To run tests:
1. Open Test Explorer in Visual Studio (Test → Test Explorer)
2. Click "Run All Tests"

## Technologies Used

- **C#** - Primary programming language
- **WPF (Windows Presentation Foundation)** - UI framework
- **XAML** - UI markup language
- **.NET Framework** - Application framework
- **CSV** - Data storage for text abbreviations


## Author

**Sourav Verma**
- GitHub: [@souravverma3738](https://github.com/souravverma3738)




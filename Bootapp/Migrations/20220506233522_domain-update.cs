using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bootapp.Migrations
{
    public partial class domainupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "file_typeid",
                table: "app_file",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "original_file_name",
                table: "app_file",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "app_file_type",
                columns: new[] { "id", "extension", "format" },
                values: new object[,]
                {
                    { 1, ".ORG", "Emacs Org Text Document" },
                    { 2, ".DOC", "Microsoft Word Document (Legacy)" },
                    { 3, ".APKG", "Exported Anki Flashcard Deck" },
                    { 4, ".STY", "LaTeX Style" },
                    { 5, ".LST", "Data List" },
                    { 6, ".ADOC", "AsciiDoc Document" },
                    { 7, ".FCF", "Final Draft Converter File" },
                    { 8, ".SAM", "LMHOSTS Sample File" },
                    { 9, ".SMF", "StarMath Formula File" },
                    { 10, ".FPT", "FoxPro Table Memo" },
                    { 11, ".MAN", "Unix Manual" },
                    { 12, ".HS", "Java HelpSet File" },
                    { 13, ".DOCX", "Microsoft Word Document" },
                    { 14, ".UPD", "Program Update Information" },
                    { 15, ".RFT", "Revisable Form Text Document" },
                    { 16, ".ODM", "OpenDocument Master Document" },
                    { 17, ".TMDX", "TextMaker Document" },
                    { 18, ".DIZ", "Description in Zip File" },
                    { 19, ".SAVE", "Nano Temporary Save File" },
                    { 20, ".COPF", "Copy Operation File" },
                    { 21, ".FAQ", "Frequently Asked Questions Document" },
                    { 22, ".DSC", "Text Description File" },
                    { 23, ".WTT", "Write! Document" },
                    { 24, ".LXFML", "LEGO Digital Designer XML File" },
                    { 25, ".MNT", "FoxPro Menu Memo" },
                    { 26, ".ME", "Readme Text File" },
                    { 27, ".GFORM", "Google Forms Shortcut" },
                    { 28, ".LTX", "LaTeX Document" },
                    { 29, ".APPODEAL", "Appodeal Text File" },
                    { 30, ".FODT", "OpenDocument Flat XML Document" },
                    { 31, ".GDOC", "Google Docs Shortcut" },
                    { 32, ".ANS", "ANSI Text File" },
                    { 33, ".GPD", "Generic Printer Description File" },
                    { 34, ".GSITE", "Google Sites Shortcut" },
                    { 35, ".APT", "Almost Plain Text File" },
                    { 36, ".TXT", "Plain Text File" },
                    { 37, ".DOCM", "Microsoft Word Macro-enabled Document" },
                    { 38, ".JARVIS", "Jarvis Subscriber File" },
                    { 39, ".TEX", "LaTeX Source Document" },
                    { 40, ".DROPBOX", "Dropbox Shared Folder Tracker" },
                    { 41, ".TLB", "VAX Text Library" },
                    { 42, ".WPS", "Microsoft Works Word Processor Document" },
                    { 43, ".RTF", "Rich Text Format File" },
                    { 44, ".XY", "XYWrite Document" },
                    { 45, ".KLG", "KOFIA Log" },
                    { 46, ".FADEIN.TEMPLATE", "Fade In Template" },
                    { 47, ".PWDPL", "Password Pad Lite Document" },
                    { 48, ".STORY", "Storyist Document" },
                    { 49, ".LUE", "Norton LiveUpdate Log File" },
                    { 50, ".FBL", "CADfix Command Level Log File" },
                    { 51, ".IPF", "OS/2 Help File" },
                    { 52, ".AIM", "AIMMS ASCII Model File" },
                    { 53, ".WP", "WordPerfect Document" },
                    { 54, ".EIO", "Yozo Office File" },
                    { 55, ".IPSPOT", "iPhoto Spot File" },
                    { 56, ".VNT", "Mobile Phone vNote File" },
                    { 57, ".TEXT", "Plain Text File" },
                    { 58, ".SIG", "Signature File" },
                    { 59, ".LOG", "Log File" },
                    { 60, ".RPT", "Generic Report" },
                    { 61, ".GSD", "General Station Description File" },
                    { 62, ".B", "Brainf*ck Source Code File" },
                    { 63, ".LST", "FoxPro Documenting Wizard List" },
                    { 64, ".WPD", "WordPerfect Document" },
                    { 65, ".ASC", "Autodesk ASCII Export File" },
                    { 66, ".SCM", "Schema File" },
                    { 67, ".OPEICO", "Opeico Text File" },
                    { 68, ".JNP", "Java Web Start File" },
                    { 69, ".HWP", "Hanword Document" },
                    { 70, ".AWW", "Ability Write Document" },
                    { 71, ".1ST", "Readme File" },
                    { 72, ".ASC", "ASCII Text File" },
                    { 73, ".RST", "reStructuredText File" },
                    { 74, ".FOUNTAIN", "Fountain Script File" },
                    { 75, ".OTT", "OpenDocument Document Template" },
                    { 76, ".EMULECOLLECTION", "eMule Data File" },
                    { 77, ".WPT", "WordPerfect Template" },
                    { 78, ".README", "Readme File" },
                    { 79, ".BIB", "BibTeX Bibliography Database" },
                    { 80, ".PAGES", "Apple Pages Document" },
                    { 81, ".WPS", "Kingsoft Writer Document" },
                    { 82, ".WPW", "WP Works Word Processor File" },
                    { 83, ".DOCZ", "ThinkFree Online Note Document" },
                    { 84, ".LUF", "Lipikar Uniform Format File" },
                    { 85, ".DXB", "Duxbury Braille File" },
                    { 86, ".BDR", "Exchange Non-Delivery Report Body File" },
                    { 87, ".GSCRIPT", "Google Apps Script Shortcut" },
                    { 88, ".ETF", "ENIGMA Transportable File" },
                    { 89, ".RAD", "Radar ViewPoint Radar Data" },
                    { 90, ".TM", "TeXmacs Document" },
                    { 91, ".QBL", "QuickBooks License File" },
                    { 92, ".BIB", "Bibliography Document" },
                    { 93, ".KNT", "KeyNote Note File" },
                    { 94, ".LIS", "SQR Output File" },
                    { 95, ".CHORD", "Song Chords File" },
                    { 96, ".TMVX", "TextMaker Document Template" },
                    { 97, ".KLG", "Log File" },
                    { 98, ".EPP", "EditPad Pro Project" },
                    { 99, ".ATY", "Association Type Placeholder" },
                    { 100, ".TFRPROJ", "theFrame Project File" },
                    { 101, ".STRINGS", "Text Strings File" },
                    { 102, ".BAD", "Exchange Badmail File" },
                    { 103, ".RIS", "Research Information Systems Citation File" },
                    { 104, ".SCC", "Scenarist Closed Caption File" },
                    { 105, ".WRI", "Microsoft Write Document" },
                    { 106, ".ODT", "OpenDocument Text Document" },
                    { 107, ".MD5.TXT", "Message Digest 5 Hash File" },
                    { 108, ".EML", "E-Mail Message" },
                    { 109, ".MSG", "Outlook Message Item File" },
                    { 110, ".BF", "Brainf*ck Source Code File" },
                    { 111, ".ERR", "Error Log File" },
                    { 112, "._DOCX", "Renamed Microsoft Word Document" },
                    { 113, ".NFO", "Warez Information File" },
                    { 114, ".RTFD", "Rich Text Format Directory File" },
                    { 115, ".FDX", "Final Draft Document" },
                    { 116, ".DM", "BYOND Dream Maker Code" },
                    { 117, ".NOTE", "Notability Note File" },
                    { 118, ".GTABLE", "Google Fusion Table Shortcut" },
                    { 119, ".TMD", "TextMaker Document" },
                    { 120, ".WP7", "WordPerfect 7 Document" },
                    { 121, ".FDT", "Final Draft 5-7 Template" },
                    { 122, ".CEC", "Studio C Alpha Upgrade File" },
                    { 123, ".RTX", "Rich Text Document" },
                    { 124, ".UTF8", "Unicode UTF8-Encoded Text Document" },
                    { 125, ".OMFL", "Open Multiple Files File List" },
                    { 126, ".KES", "Kurzweil 3000 Document" },
                    { 127, ".BDP", "Exchange Diagnostic Message" },
                    { 128, ".GMAP", "Google My Maps Shortcut" },
                    { 129, ".RUN", "Runscanner Scan File" },
                    { 130, ".SAF", "SafeText File" },
                    { 131, ".SE", "Shuttle Document" },
                    { 132, ".LP2", "iLEAP Word Processing Document" },
                    { 133, ".DFTI", "FlexiWrite Document" },
                    { 134, ".PAGES-TEF", "Pages iCloud Document" },
                    { 135, ".BEAN", "Bean Rich Text Document" },
                    { 136, ".PWD", "Pocket Word Document" },
                    { 137, ".SGM", "SGML File" },
                    { 138, ".ETX", "Structure Enhanced Text (Setext) File" },
                    { 139, ".U3I", "U3 Application Information File" },
                    { 140, ".FRT", "FoxPro Report Memo" },
                    { 141, ".FDR", "Final Draft Document" },
                    { 142, ".CHARSET", "Character Set" },
                    { 143, ".TEXTCLIPPING", "Mac OS X Text Clipping File" },
                    { 144, ".MBOX", "Email Mailbox" },
                    { 145, ".COD", "Atlantis Word Processor Encrypted Document" },
                    { 146, ".SXW", "StarOffice Writer Document" },
                    { 147, ".STW", "StarOffice Document Template" },
                    { 148, ".DX", "DEC WPS Plus File" },
                    { 149, ".EMLX", "Apple Mail Message" },
                    { 150, ".ABW", "AbiWord Document" },
                    { 151, ".RVF", "RichView Format File" },
                    { 152, ".DVI", "Device Independent Format File" },
                    { 153, ".JIS", "Japanese Industry Standard Text" },
                    { 154, ".XWP", "XMLwriter Project" },
                    { 155, ".ODIF", "Open Document Interchange Format" },
                    { 156, ".SDM", "StarOffice Mail Message" },
                    { 157, ".NGLOSS", "Nisus Writer Glossary" },
                    { 158, ".GSLIDES", "Google Slides Shortcut" },
                    { 159, ".602", "Text602 Document" },
                    { 160, ".WPT", "Kingsoft Writer Template" },
                    { 161, ".XWP", "Xerox Writer Text Document" },
                    { 162, ".NDOC", "Naver Word" },
                    { 163, ".PLAIN", "Plain Text File" },
                    { 164, ".PSW", "Pocket Word Document" },
                    { 165, ".P7S", "Digitally Signed Email Message" },
                    { 166, ".SLA", "Scribus Document" },
                    { 167, ".TAB", "Tab Separated Data File" },
                    { 168, ".SUBLIME-PROJECT", "Sublime Text Project File" },
                    { 169, ".EIT", "Yozo Office Template File" },
                    { 170, ".LATEX", "LaTeX Document" },
                    { 171, ".QDL", "QDL Program" },
                    { 172, ".TRELBY", "Trelby File" },
                    { 173, ".HWP", "Hangul (Korean) Text Document" },
                    { 174, ".KON", "Yahoo! Widget XML File" },
                    { 175, ".SMS", "Exported SMS Text Message" },
                    { 176, ".DOCXML", "Microsoft Word XML Document" },
                    { 177, ".ODO", "Online Operating System Write Document" },
                    { 178, ".TEMPLATE", "Apple Pages Template" },
                    { 179, ".SDOC", "Satra Khmer Document" },
                    { 180, ".IDX", "Outlook Express Mailbox Index File" },
                    { 181, ".BIBTEX", "BibTeX Bibliography Database" },
                    { 182, ".PWI", "Pocket Word Document" },
                    { 183, ".TPC", "Topic Connection Placeholder" },
                    { 184, ".XYW", "XYWrite for Windows Document" },
                    { 185, ".NOW", "Readme File" },
                    { 186, ".RZK", "File Crypt Password File" },
                    { 187, ".BOC", "EasyWord Big Document" },
                    { 188, ".DGS", "Dagesh Pro Document" },
                    { 189, ".PRT", "Printer Output File" },
                    { 190, ".LBT", "FoxPro Label Memo" },
                    { 191, ".HBK", "Mathcad Handbook File" },
                    { 192, ".TAB", "Guitar Tablature File" },
                    { 193, ".PWR", "PowerWrite Document" },
                    { 194, ".PVM", "Photo Video Manifest File" },
                    { 195, ".BML", "Braille 2000 Braille File" },
                    { 196, ".JP1", "Japanese (Romaji) Text File" },
                    { 197, ".SESSION", "Mozilla Firefox Session File" },
                    { 198, ".MD", "MuseData Musical Score" },
                    { 199, ".BXT", "Balabolka Text Document" },
                    { 200, ".WP4", "WordPerfect 4 Document" },
                    { 201, ".RZN", "Red Zion Notes File" },
                    { 202, ".MPD", "MPEG-DASH Media Presentation Description" },
                    { 203, ".NOTES", "Memento Notes File" },
                    { 204, ".WPD", "602Text Word Processing Document" },
                    { 205, ".MWP", "Lotus Word Pro SmartMaster File" },
                    { 206, ".MSS", "CartoCSS Map Stylesheet" },
                    { 207, ".HIGHLAND", "Highland Document" },
                    { 208, ".SCRIVX", "Scrivener XML Document" },
                    { 209, ".HHT", "Help and Support Center HHT File" },
                    { 210, ".SCRIV", "Scrivener Document" },
                    { 211, ".MWD", "Mariner Write Document" },
                    { 212, ".LNK42", "Windows 93 Desktop Shortcut" },
                    { 213, ".CALCA", "Calca Document" },
                    { 214, ".SLA.GZ", "Scribus Compressed Document" },
                    { 215, ".MELLEL", "Mellel Word Processing Document" },
                    { 216, ".BNA", "Barna Word Processor Document" },
                    { 217, ".VCF", "Variant Call Format File" },
                    { 218, ".FDF", "Acrobat Forms Data Format" },
                    { 219, ".ZRTF", "Nisus Compressed Rich Text File" },
                    { 220, ".DCA", "DisplayWrite Document" },
                    { 221, ".MW", "MacWrite Text Document" },
                    { 222, ".UOT", "Uniform Office Document" },
                    { 223, ".FLR", "Flare Decompiled ActionScript File" },
                    { 224, ".BTD", "Business-in-a-Box Document" },
                    { 225, ".XBDOC", "Xiosis Scribe Document" },
                    { 226, ".UTXT", "Unicode Text File" },
                    { 227, ".CNM", "NoteMap Outline File" },
                    { 228, ".ERR", "FoxPro Compilation Error" },
                    { 229, "._DOC", "Renamed Microsoft Word Document" },
                    { 230, ".SAFETEXT", "SafeText File" },
                    { 231, ".JTD", "JustSystems Ichitaro Document" },
                    { 232, ".MELL", "Mellel Word Processing File" },
                    { 233, ".OFL", "Ots File List" },
                    { 234, ".ACT", "FoxPro Documenting Wizard Action Diagram" },
                    { 235, ".LYX", "LyX Document" },
                    { 236, ".WPD", "ACT! 2 Word Processing Document" },
                    { 237, ".WPL", "DEC WPS Plus Text Document" },
                    { 238, ".CAST", "Asciicast Terminal Recording" },
                    { 239, ".EUC", "Extended Unix Code File" },
                    { 240, ".HZ", "Chinese (Hanzi) Text" },
                    { 241, ".ORT", "Rich Text Editor Document" },
                    { 242, ".NJX", "NJStar Document" },
                    { 243, ".WBK", "WordPerfect Workbook" },
                    { 244, ".XDL", "Oracle Expert Definition Language File" },
                    { 245, ".UNAUTH", "SiteMinder Unauthorized Message File" },
                    { 246, ".FFT", "Final Form Text File" },
                    { 247, ".WP6", "WordPerfect 6 Document" },
                    { 248, ".DWD", "DavkaWriter File" },
                    { 249, ".LWP", "Lotus Word Pro Document" },
                    { 250, ".PRT", "Crypt Edit Protected Text Format File" },
                    { 251, ".FWDN", "fWriter Document" },
                    { 252, ".JOE", "JOE Document" },
                    { 253, ".RTD", "RagTime Document" },
                    { 254, ".ZABW", "Compressed AbiWord Document File" },
                    { 255, ".AWP", "Ability Write Template" },
                    { 256, ".TMV", "TextMaker Template" },
                    { 257, ".GPN", "GlidePlan Map Document" },
                    { 258, ".SAM", "Ami Pro Document" },
                    { 259, ".TDF", "Guide Text Definition File" },
                    { 260, ".PMO", "Pegasus Saved Message File" },
                    { 261, ".AWT", "AbiWord Template" },
                    { 262, ".XBPLATE", "Xiosis Scribe Template" },
                    { 263, ".GJAM", "Google Jamboard Shortcut" },
                    { 264, ".LICENSE", "Software License File" },
                    { 265, ".PU", "PlantUML File" },
                    { 266, ".QUID", "QuidProQuo Document" },
                    { 267, ".GV", "Graphviz DOT File" },
                    { 268, ".SDW", "StarOffice Writer Text Document" },
                    { 269, ".SXG", "Apache OpenOffice Master Document" },
                    { 270, ".OPENBSD", "OpenBSD Readme File" },
                    { 271, ".PLANTUML", "PlantUML File" },
                    { 272, ".CRWL", "Windows Crawl File" },
                    { 273, ".ASCII", "ASCII Text File" },
                    { 274, ".NB", "Nota Bene File" },
                    { 275, ".PIMX", "Adobe Package Installation Management File" },
                    { 276, ".ASE", "Autodesk ASCII Scene Export File" },
                    { 277, ".VCT", "Visual Class Library Memo" },
                    { 278, ".XYP", "XYWrite Plus Document" },
                    { 279, ".XWP", "Crosstalk Session File" },
                    { 280, ".DEL", "Delimited ASCII File" },
                    { 281, ".EBP", "Express Burn Project" },
                    { 282, ".SCT", "FoxPro Form Memo" },
                    { 283, ".LYT", "TurboTax Install Log File" },
                    { 284, ".OCR", "FAXGrapper Fax Text File" },
                    { 285, ".BBS", "Bulletin Board System Text" },
                    { 286, ".TDF", "Xserve Test Definition File" },
                    { 287, ".DNE", "Netica Text File" },
                    { 288, ".WEBDOC", "Box.net Web Document" },
                    { 289, ".CWS", "Claris Works Template" },
                    { 290, ".FDXT", "Final Draft 8 Template" },
                    { 291, ".LNT", "Laego Note Taker File" },
                    { 292, ".QPF", "QuickPad Encrypted Document" },
                    { 293, ".SUBLIME-WORKSPACE", "Sublime Text Workspace File" },
                    { 294, ".GTHR", "Gather Log File" },
                    { 295, ".UNX", "Unix Text File" },
                    { 296, ".DESCRIPTION", "youtube-dl Video Description" },
                    { 297, ".NWP", "Now Contact WP Document" },
                    { 298, ".WSD", "WordStar Document" },
                    { 299, ".XDL", "XML Schema File" },
                    { 300, ".BRX", "Beam Report Document" },
                    { 301, ".KWD", "KWord Document" },
                    { 302, ".SKCARD", "Starfish Sidekick Card File" },
                    { 303, ".DXP", "Duxbury Print File" },
                    { 304, ".XY3", "XYWrite III Document" },
                    { 305, ".UOF", "Uniform Office Document" },
                    { 306, ".PTNX", "EasyBeadPatterns Pattern" },
                    { 307, ".SW3", "Scriptware Screenplay" },
                    { 308, ".FGS", "Fig Figure Settings File" },
                    { 309, ".TID", "TiddlyWiki Tiddler" },
                    { 310, ".PWDP", "Password Pad Document" },
                    { 311, ".VPDOC", "VoodooPad Document" },
                    { 312, ".NWM", "Nisus Macro" },
                    { 313, ".FDS", "Final Draft Secure Copy" },
                    { 314, ".CYI", "Clustify Input File" },
                    { 315, ".PVJ", "ProofVision Job Ticket" },
                    { 316, ".PFX", "First Choice Word Processing Document" },
                    { 317, ".EMF", "Jasspa MicroEmacs Macro File" },
                    { 318, ".WP5", "WordPerfect 5 Document" },
                    { 319, ".WTX", "Text Document" },
                    { 320, ".ZW", "Chinese Text File" },
                    { 321, ".WPA", "ACT! Word Processing Document" },
                    { 322, ".JRTF", "JAmes OS Rich Text File" },
                    { 323, ".LTR", "Letter File" },
                    { 324, ".MIN", "Mint Source File" },
                    { 325, ".PDPCMD", "Pdplayer Command File" },
                    { 326, ".WN", "WriteNow Text Document" },
                    { 327, ".SCW", "Movie Magic Screenwriter Document" },
                    { 328, ".BZABW", "Compressed AbiWord Document" },
                    { 329, ".TVJ", "TrueView Job Ticket" },
                    { 330, ".IIL", "CleanSweep Installation Log" },
                    { 331, ".ODP", "Organ Definition Project" },
                    { 332, ".VW", "Volkswriter Text File" },
                    { 333, ".NWCTXT", "NoteWorthy Composer Text File" },
                    { 334, ".GMD", "GroupMail Message" },
                    { 335, ".MCW", "MacWrite II Document" },
                    { 336, ".DOX", "MultiMate Document" },
                    { 337, ".THP", "TurboTax Text String" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_file_file_typeid",
                table: "app_file",
                column: "file_typeid");

            migrationBuilder.AddForeignKey(
                name: "FK_app_file_app_file_type_file_typeid",
                table: "app_file",
                column: "file_typeid",
                principalTable: "app_file_type",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_file_app_file_type_file_typeid",
                table: "app_file");

            migrationBuilder.DropIndex(
                name: "IX_app_file_file_typeid",
                table: "app_file");

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "app_file_type",
                keyColumn: "id",
                keyValue: 337);

            migrationBuilder.DropColumn(
                name: "file_typeid",
                table: "app_file");

            migrationBuilder.DropColumn(
                name: "original_file_name",
                table: "app_file");
        }
    }
}

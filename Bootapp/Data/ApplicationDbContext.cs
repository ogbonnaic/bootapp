using System;
using Bootapp.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bootapp.Data
{
    public class ApplicationDbContext : AuditableIdentityContext
    {
        //public ApplicationDbContext()
        //{
        //}

 


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<app_account> app_account { set; get; }
        public DbSet<app_project> app_project { get; set; }
        public DbSet<app_datasource> app_datasource { set; get; }
        public DbSet<app_project_table_setting> app_project_table_setting { set; get; }
        public DbSet<app_setting> app_setting { set; get; }
        public DbSet<app_connection> app_connection { set; get; }
        public DbSet<app_short_url> app_short_url { set; get; }
        public DbSet<app_file> app_file { set; get; }
        public DbSet<app_file_type> app_file_type { set; get; }
        public DbSet<app_domain> app_domain { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(GetConnectionString());

            if (!optionsBuilder.IsConfigured)
            {

                IConfigurationRoot configuration = new ConfigurationBuilder()
                                                                             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                                             .AddJsonFile("appsettings.json")
                                                                             .Build();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            }

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("app_project_table_setting_id")
              .StartsAt(1)
              .IncrementsBy(1);

            modelBuilder.Entity<app_project_table_setting>()
                        .Property(o => o.id)
                        .HasDefaultValueSql("nextval('\"app_project_table_setting_id\"')");

            modelBuilder.Entity<app_project_table_setting>()
                .Property(b => b.delete)
                .HasDefaultValue(false);

            modelBuilder.Entity<app_project_table_setting>()
             .Property(b => b.create)
             .HasDefaultValue(true);

            modelBuilder.Entity<app_project_table_setting>()
             .Property(b => b.update)
             .HasDefaultValue(true);

            modelBuilder.Entity<app_project_table_setting>()
             .Property(b => b.read)
             .HasDefaultValue(true);
            //     modelBuilder.Entity<IdentityUser>()
            // .ToTable("AspNetUsers", t => t.ExcludeFromMigrations());

            //     modelBuilder.Entity<IdentityUserLogin<string>>()
            //.ToTable("AspNetUsers", t => t.ExcludeFromMigrations());


            modelBuilder.HasSequence<int>("app_setting_id")
           .StartsAt(1)
           .IncrementsBy(1);

            modelBuilder.Entity<app_setting>()
                      .Property(o => o.id)
                      .HasDefaultValueSql("nextval('\"app_setting_id\"')");

            modelBuilder.Entity<app_datasource>()
                .HasData(
                    new app_datasource
                    {
                        id = 1,
                        name = "PostgreSQL",
                        active = 1
                    },
                    new app_datasource
                    {
                        id = 2,
                        name = "MySQL",
                        active = 1
                    },
                     new app_datasource
                     {
                         id = 3,
                         name = "SQL Server",
                         active = 1
                     }
                );

            modelBuilder.Entity<app_file_type>()
                .HasData(

 new app_file_type
 {
     id = 1,
     extension = ".ORG",
     format = "Emacs Org Text Document"
 },
 new app_file_type
 {
     id = 2,
     extension = ".DOC",
     format = "Microsoft Word Document (Legacy)"
 },
 new app_file_type
 {
     id = 3,
     extension = ".APKG",
     format = "Exported Anki Flashcard Deck"
 },
 new app_file_type
 {
     id = 4,
     extension = ".STY",
     format = "LaTeX Style"
 },
 new app_file_type
 {
     id = 5,
     extension = ".LST",
     format = "Data List"
 },
 new app_file_type
 {
     id = 6,
     extension = ".ADOC",
     format = "AsciiDoc Document"
 },
 new app_file_type
 {
     id = 7,
     extension = ".FCF",
     format = "Final Draft Converter File"
 },
 new app_file_type
 {
     id = 8,
     extension = ".SAM",
     format = "LMHOSTS Sample File"
 },
 new app_file_type
 {
     id = 9,
     extension = ".SMF",
     format = "StarMath Formula File"
 },
 new app_file_type
 {
     id = 10,
     extension = ".FPT",
     format = "FoxPro Table Memo"
 },
 new app_file_type
 {
     id = 11,
     extension = ".MAN",
     format = "Unix Manual"
 },
 new app_file_type
 {
     id = 12,
     extension = ".HS",
     format = "Java HelpSet File"
 },
 new app_file_type
 {
     id = 13,
     extension = ".DOCX",
     format = "Microsoft Word Document"
 },
 new app_file_type
 {
     id = 14,
     extension = ".UPD",
     format = "Program Update Information"
 },
 new app_file_type
 {
     id = 15,
     extension = ".RFT",
     format = "Revisable Form Text Document"
 },
 new app_file_type
 {
     id = 16,
     extension = ".ODM",
     format = "OpenDocument Master Document"
 },
 new app_file_type
 {
     id = 17,
     extension = ".TMDX",
     format = "TextMaker Document"
 },
 new app_file_type
 {
     id = 18,
     extension = ".DIZ",
     format = "Description in Zip File"
 },
 new app_file_type
 {
     id = 19,
     extension = ".SAVE",
     format = "Nano Temporary Save File"
 },
 new app_file_type
 {
     id = 20,
     extension = ".COPF",
     format = "Copy Operation File"
 },
 new app_file_type
 {
     id = 21,
     extension = ".FAQ",
     format = "Frequently Asked Questions Document"
 },
 new app_file_type
 {
     id = 22,
     extension = ".DSC",
     format = "Text Description File"
 },
 new app_file_type
 {
     id = 23,
     extension = ".WTT",
     format = "Write! Document"
 },
 new app_file_type
 {
     id = 24,
     extension = ".LXFML",
     format = "LEGO Digital Designer XML File"
 },
 new app_file_type
 {
     id = 25,
     extension = ".MNT",
     format = "FoxPro Menu Memo"
 },
 new app_file_type
 {
     id = 26,
     extension = ".ME",
     format = "Readme Text File"
 },
 new app_file_type
 {
     id = 27,
     extension = ".GFORM",
     format = "Google Forms Shortcut"
 },
 new app_file_type
 {
     id = 28,
     extension = ".LTX",
     format = "LaTeX Document"
 },
 new app_file_type
 {
     id = 29,
     extension = ".APPODEAL",
     format = "Appodeal Text File"
 },
 new app_file_type
 {
     id = 30,
     extension = ".FODT",
     format = "OpenDocument Flat XML Document"
 },
 new app_file_type
 {
     id = 31,
     extension = ".GDOC",
     format = "Google Docs Shortcut"
 },
 new app_file_type
 {
     id = 32,
     extension = ".ANS",
     format = "ANSI Text File"
 },
 new app_file_type
 {
     id = 33,
     extension = ".GPD",
     format = "Generic Printer Description File"
 },
 new app_file_type
 {
     id = 34,
     extension = ".GSITE",
     format = "Google Sites Shortcut"
 },
 new app_file_type
 {
     id = 35,
     extension = ".APT",
     format = "Almost Plain Text File"
 },
 new app_file_type
 {
     id = 36,
     extension = ".TXT",
     format = "Plain Text File"
 },
 new app_file_type
 {
     id = 37,
     extension = ".DOCM",
     format = "Microsoft Word Macro-enabled Document"
 },
 new app_file_type
 {
     id = 38,
     extension = ".JARVIS",
     format = "Jarvis Subscriber File"
 },
 new app_file_type
 {
     id = 39,
     extension = ".TEX",
     format = "LaTeX Source Document"
 },
 new app_file_type
 {
     id = 40,
     extension = ".DROPBOX",
     format = "Dropbox Shared Folder Tracker"
 },
 new app_file_type
 {
     id = 41,
     extension = ".TLB",
     format = "VAX Text Library"
 },
 new app_file_type
 {
     id = 42,
     extension = ".WPS",
     format = "Microsoft Works Word Processor Document"
 },
 new app_file_type
 {
     id = 43,
     extension = ".RTF",
     format = "Rich Text Format File"
 },
 new app_file_type
 {
     id = 44,
     extension = ".XY",
     format = "XYWrite Document"
 },
 new app_file_type
 {
     id = 45,
     extension = ".KLG",
     format = "KOFIA Log"
 },
 new app_file_type
 {
     id = 46,
     extension = ".FADEIN.TEMPLATE",
     format = "Fade In Template"
 },
 new app_file_type
 {
     id = 47,
     extension = ".PWDPL",
     format = "Password Pad Lite Document"
 },
 new app_file_type
 {
     id = 48,
     extension = ".STORY",
     format = "Storyist Document"
 },
 new app_file_type
 {
     id = 49,
     extension = ".LUE",
     format = "Norton LiveUpdate Log File"
 },
 new app_file_type
 {
     id = 50,
     extension = ".FBL",
     format = "CADfix Command Level Log File"
 },
 new app_file_type
 {
     id = 51,
     extension = ".IPF",
     format = "OS/2 Help File"
 },
 new app_file_type
 {
     id = 52,
     extension = ".AIM",
     format = "AIMMS ASCII Model File"
 },
 new app_file_type
 {
     id = 53,
     extension = ".WP",
     format = "WordPerfect Document"
 },
 new app_file_type
 {
     id = 54,
     extension = ".EIO",
     format = "Yozo Office File"
 },
 new app_file_type
 {
     id = 55,
     extension = ".IPSPOT",
     format = "iPhoto Spot File"
 },
 new app_file_type
 {
     id = 56,
     extension = ".VNT",
     format = "Mobile Phone vNote File"
 },
 new app_file_type
 {
     id = 57,
     extension = ".TEXT",
     format = "Plain Text File"
 },
 new app_file_type
 {
     id = 58,
     extension = ".SIG",
     format = "Signature File"
 },
 new app_file_type
 {
     id = 59,
     extension = ".LOG",
     format = "Log File"
 },
 new app_file_type
 {
     id = 60,
     extension = ".RPT",
     format = "Generic Report"
 },
 new app_file_type
 {
     id = 61,
     extension = ".GSD",
     format = "General Station Description File"
 },
 new app_file_type
 {
     id = 62,
     extension = ".B",
     format = "Brainf*ck Source Code File"
 },
 new app_file_type
 {
     id = 63,
     extension = ".LST",
     format = "FoxPro Documenting Wizard List"
 },
 new app_file_type
 {
     id = 64,
     extension = ".WPD",
     format = "WordPerfect Document"
 },
 new app_file_type
 {
     id = 65,
     extension = ".ASC",
     format = "Autodesk ASCII Export File"
 },
 new app_file_type
 {
     id = 66,
     extension = ".SCM",
     format = "Schema File"
 },
 new app_file_type
 {
     id = 67,
     extension = ".OPEICO",
     format = "Opeico Text File"
 },
 new app_file_type
 {
     id = 68,
     extension = ".JNP",
     format = "Java Web Start File"
 },
 new app_file_type
 {
     id = 69,
     extension = ".HWP",
     format = "Hanword Document"
 },
 new app_file_type
 {
     id = 70,
     extension = ".AWW",
     format = "Ability Write Document"
 },
 new app_file_type
 {
     id = 71,
     extension = ".1ST",
     format = "Readme File"
 },
 new app_file_type
 {
     id = 72,
     extension = ".ASC",
     format = "ASCII Text File"
 },
 new app_file_type
 {
     id = 73,
     extension = ".RST",
     format = "reStructuredText File"
 },
 new app_file_type
 {
     id = 74,
     extension = ".FOUNTAIN",
     format = "Fountain Script File"
 },
 new app_file_type
 {
     id = 75,
     extension = ".OTT",
     format = "OpenDocument Document Template"
 },
 new app_file_type
 {
     id = 76,
     extension = ".EMULECOLLECTION",
     format = "eMule Data File"
 },
 new app_file_type
 {
     id = 77,
     extension = ".WPT",
     format = "WordPerfect Template"
 },
 new app_file_type
 {
     id = 78,
     extension = ".README",
     format = "Readme File"
 },
 new app_file_type
 {
     id = 79,
     extension = ".BIB",
     format = "BibTeX Bibliography Database"
 },
 new app_file_type
 {
     id = 80,
     extension = ".PAGES",
     format = "Apple Pages Document"
 },
 new app_file_type
 {
     id = 81,
     extension = ".WPS",
     format = "Kingsoft Writer Document"
 },
 new app_file_type
 {
     id = 82,
     extension = ".WPW",
     format = "WP Works Word Processor File"
 },
 new app_file_type
 {
     id = 83,
     extension = ".DOCZ",
     format = "ThinkFree Online Note Document"
 },
 new app_file_type
 {
     id = 84,
     extension = ".LUF",
     format = "Lipikar Uniform Format File"
 },
 new app_file_type
 {
     id = 85,
     extension = ".DXB",
     format = "Duxbury Braille File"
 },
 new app_file_type
 {
     id = 86,
     extension = ".BDR",
     format = "Exchange Non-Delivery Report Body File"
 },
 new app_file_type
 {
     id = 87,
     extension = ".GSCRIPT",
     format = "Google Apps Script Shortcut"
 },
 new app_file_type
 {
     id = 88,
     extension = ".ETF",
     format = "ENIGMA Transportable File"
 },
 new app_file_type
 {
     id = 89,
     extension = ".RAD",
     format = "Radar ViewPoint Radar Data"
 },
 new app_file_type
 {
     id = 90,
     extension = ".TM",
     format = "TeXmacs Document"
 },
 new app_file_type
 {
     id = 91,
     extension = ".QBL",
     format = "QuickBooks License File"
 },
 new app_file_type
 {
     id = 92,
     extension = ".BIB",
     format = "Bibliography Document"
 },
 new app_file_type
 {
     id = 93,
     extension = ".KNT",
     format = "KeyNote Note File"
 },
 new app_file_type
 {
     id = 94,
     extension = ".LIS",
     format = "SQR Output File"
 },
 new app_file_type
 {
     id = 95,
     extension = ".CHORD",
     format = "Song Chords File"
 },
 new app_file_type
 {
     id = 96,
     extension = ".TMVX",
     format = "TextMaker Document Template"
 },
 new app_file_type
 {
     id = 97,
     extension = ".KLG",
     format = "Log File"
 },
 new app_file_type
 {
     id = 98,
     extension = ".EPP",
     format = "EditPad Pro Project"
 },
 new app_file_type
 {
     id = 99,
     extension = ".ATY",
     format = "Association Type Placeholder"
 },
 new app_file_type
 {
     id = 100,
     extension = ".TFRPROJ",
     format = "theFrame Project File"
 },
 new app_file_type
 {
     id = 101,
     extension = ".STRINGS",
     format = "Text Strings File"
 },
 new app_file_type
 {
     id = 102,
     extension = ".BAD",
     format = "Exchange Badmail File"
 },
 new app_file_type
 {
     id = 103,
     extension = ".RIS",
     format = "Research Information Systems Citation File"
 },
 new app_file_type
 {
     id = 104,
     extension = ".SCC",
     format = "Scenarist Closed Caption File"
 },
 new app_file_type
 {
     id = 105,
     extension = ".WRI",
     format = "Microsoft Write Document"
 },
 new app_file_type
 {
     id = 106,
     extension = ".ODT",
     format = "OpenDocument Text Document"
 },
 new app_file_type
 {
     id = 107,
     extension = ".MD5.TXT",
     format = "Message Digest 5 Hash File"
 },
 new app_file_type
 {
     id = 108,
     extension = ".EML",
     format = "E-Mail Message"
 },
 new app_file_type
 {
     id = 109,
     extension = ".MSG",
     format = "Outlook Message Item File"
 },
 new app_file_type
 {
     id = 110,
     extension = ".BF",
     format = "Brainf*ck Source Code File"
 },
 new app_file_type
 {
     id = 111,
     extension = ".ERR",
     format = "Error Log File"
 },
 new app_file_type
 {
     id = 112,
     extension = "._DOCX",
     format = "Renamed Microsoft Word Document"
 },
 new app_file_type
 {
     id = 113,
     extension = ".NFO",
     format = "Warez Information File"
 },
 new app_file_type
 {
     id = 114,
     extension = ".RTFD",
     format = "Rich Text Format Directory File"
 },
 new app_file_type
 {
     id = 115,
     extension = ".FDX",
     format = "Final Draft Document"
 },
 new app_file_type
 {
     id = 116,
     extension = ".DM",
     format = "BYOND Dream Maker Code"
 },
 new app_file_type
 {
     id = 117,
     extension = ".NOTE",
     format = "Notability Note File"
 },
 new app_file_type
 {
     id = 118,
     extension = ".GTABLE",
     format = "Google Fusion Table Shortcut"
 },
 new app_file_type
 {
     id = 119,
     extension = ".TMD",
     format = "TextMaker Document"
 },
 new app_file_type
 {
     id = 120,
     extension = ".WP7",
     format = "WordPerfect 7 Document"
 },
 new app_file_type
 {
     id = 121,
     extension = ".FDT",
     format = "Final Draft 5-7 Template"
 },
 new app_file_type
 {
     id = 122,
     extension = ".CEC",
     format = "Studio C Alpha Upgrade File"
 },
 new app_file_type
 {
     id = 123,
     extension = ".RTX",
     format = "Rich Text Document"
 },
 new app_file_type
 {
     id = 124,
     extension = ".UTF8",
     format = "Unicode UTF8-Encoded Text Document"
 },
 new app_file_type
 {
     id = 125,
     extension = ".OMFL",
     format = "Open Multiple Files File List"
 },
 new app_file_type
 {
     id = 126,
     extension = ".KES",
     format = "Kurzweil 3000 Document"
 },
 new app_file_type
 {
     id = 127,
     extension = ".BDP",
     format = "Exchange Diagnostic Message"
 },
 new app_file_type
 {
     id = 128,
     extension = ".GMAP",
     format = "Google My Maps Shortcut"
 },
 new app_file_type
 {
     id = 129,
     extension = ".RUN",
     format = "Runscanner Scan File"
 },
 new app_file_type
 {
     id = 130,
     extension = ".SAF",
     format = "SafeText File"
 },
 new app_file_type
 {
     id = 131,
     extension = ".SE",
     format = "Shuttle Document"
 },
 new app_file_type
 {
     id = 132,
     extension = ".LP2",
     format = "iLEAP Word Processing Document"
 },
 new app_file_type
 {
     id = 133,
     extension = ".DFTI",
     format = "FlexiWrite Document"
 },
 new app_file_type
 {
     id = 134,
     extension = ".PAGES-TEF",
     format = "Pages iCloud Document"
 },
 new app_file_type
 {
     id = 135,
     extension = ".BEAN",
     format = "Bean Rich Text Document"
 },
 new app_file_type
 {
     id = 136,
     extension = ".PWD",
     format = "Pocket Word Document"
 },
 new app_file_type
 {
     id = 137,
     extension = ".SGM",
     format = "SGML File"
 },
 new app_file_type
 {
     id = 138,
     extension = ".ETX",
     format = "Structure Enhanced Text (Setext) File"
 },
 new app_file_type
 {
     id = 139,
     extension = ".U3I",
     format = "U3 Application Information File"
 },
 new app_file_type
 {
     id = 140,
     extension = ".FRT",
     format = "FoxPro Report Memo"
 },
 new app_file_type
 {
     id = 141,
     extension = ".FDR",
     format = "Final Draft Document"
 },
 new app_file_type
 {
     id = 142,
     extension = ".CHARSET",
     format = "Character Set"
 },
 new app_file_type
 {
     id = 143,
     extension = ".TEXTCLIPPING",
     format = "Mac OS X Text Clipping File"
 },
 new app_file_type
 {
     id = 144,
     extension = ".MBOX",
     format = "Email Mailbox"
 },
 new app_file_type
 {
     id = 145,
     extension = ".COD",
     format = "Atlantis Word Processor Encrypted Document"
 },
 new app_file_type
 {
     id = 146,
     extension = ".SXW",
     format = "StarOffice Writer Document"
 },
 new app_file_type
 {
     id = 147,
     extension = ".STW",
     format = "StarOffice Document Template"
 },
 new app_file_type
 {
     id = 148,
     extension = ".DX",
     format = "DEC WPS Plus File"
 },
 new app_file_type
 {
     id = 149,
     extension = ".EMLX",
     format = "Apple Mail Message"
 },
 new app_file_type
 {
     id = 150,
     extension = ".ABW",
     format = "AbiWord Document"
 },
 new app_file_type
 {
     id = 151,
     extension = ".RVF",
     format = "RichView Format File"
 },
 new app_file_type
 {
     id = 152,
     extension = ".DVI",
     format = "Device Independent Format File"
 },
 new app_file_type
 {
     id = 153,
     extension = ".JIS",
     format = "Japanese Industry Standard Text"
 },
 new app_file_type
 {
     id = 154,
     extension = ".XWP",
     format = "XMLwriter Project"
 },
 new app_file_type
 {
     id = 155,
     extension = ".ODIF",
     format = "Open Document Interchange Format"
 },
 new app_file_type
 {
     id = 156,
     extension = ".SDM",
     format = "StarOffice Mail Message"
 },
 new app_file_type
 {
     id = 157,
     extension = ".NGLOSS",
     format = "Nisus Writer Glossary"
 },
 new app_file_type
 {
     id = 158,
     extension = ".GSLIDES",
     format = "Google Slides Shortcut"
 },
 new app_file_type
 {
     id = 159,
     extension = ".602",
     format = "Text602 Document"
 },
 new app_file_type
 {
     id = 160,
     extension = ".WPT",
     format = "Kingsoft Writer Template"
 },
 new app_file_type
 {
     id = 161,
     extension = ".XWP",
     format = "Xerox Writer Text Document"
 },
 new app_file_type
 {
     id = 162,
     extension = ".NDOC",
     format = "Naver Word"
 },
 new app_file_type
 {
     id = 163,
     extension = ".PLAIN",
     format = "Plain Text File"
 },
 new app_file_type
 {
     id = 164,
     extension = ".PSW",
     format = "Pocket Word Document"
 },
 new app_file_type
 {
     id = 165,
     extension = ".P7S",
     format = "Digitally Signed Email Message"
 },
 new app_file_type
 {
     id = 166,
     extension = ".SLA",
     format = "Scribus Document"
 },
 new app_file_type
 {
     id = 167,
     extension = ".TAB",
     format = "Tab Separated Data File"
 },
 new app_file_type
 {
     id = 168,
     extension = ".SUBLIME-PROJECT",
     format = "Sublime Text Project File"
 },
 new app_file_type
 {
     id = 169,
     extension = ".EIT",
     format = "Yozo Office Template File"
 },
 new app_file_type
 {
     id = 170,
     extension = ".LATEX",
     format = "LaTeX Document"
 },
 new app_file_type
 {
     id = 171,
     extension = ".QDL",
     format = "QDL Program"
 },
 new app_file_type
 {
     id = 172,
     extension = ".TRELBY",
     format = "Trelby File"
 },
 new app_file_type
 {
     id = 173,
     extension = ".HWP",
     format = "Hangul (Korean) Text Document"
 },
 new app_file_type
 {
     id = 174,
     extension = ".KON",
     format = "Yahoo! Widget XML File"
 },
 new app_file_type
 {
     id = 175,
     extension = ".SMS",
     format = "Exported SMS Text Message"
 },
 new app_file_type
 {
     id = 176,
     extension = ".DOCXML",
     format = "Microsoft Word XML Document"
 },
 new app_file_type
 {
     id = 177,
     extension = ".ODO",
     format = "Online Operating System Write Document"
 },
 new app_file_type
 {
     id = 178,
     extension = ".TEMPLATE",
     format = "Apple Pages Template"
 },
 new app_file_type
 {
     id = 179,
     extension = ".SDOC",
     format = "Satra Khmer Document"
 },
 new app_file_type
 {
     id = 180,
     extension = ".IDX",
     format = "Outlook Express Mailbox Index File"
 },
 new app_file_type
 {
     id = 181,
     extension = ".BIBTEX",
     format = "BibTeX Bibliography Database"
 },
 new app_file_type
 {
     id = 182,
     extension = ".PWI",
     format = "Pocket Word Document"
 },
 new app_file_type
 {
     id = 183,
     extension = ".TPC",
     format = "Topic Connection Placeholder"
 },
 new app_file_type
 {
     id = 184,
     extension = ".XYW",
     format = "XYWrite for Windows Document"
 },
 new app_file_type
 {
     id = 185,
     extension = ".NOW",
     format = "Readme File"
 },
 new app_file_type
 {
     id = 186,
     extension = ".RZK",
     format = "File Crypt Password File"
 },
 new app_file_type
 {
     id = 187,
     extension = ".BOC",
     format = "EasyWord Big Document"
 },
 new app_file_type
 {
     id = 188,
     extension = ".DGS",
     format = "Dagesh Pro Document"
 },
 new app_file_type
 {
     id = 189,
     extension = ".PRT",
     format = "Printer Output File"
 },
 new app_file_type
 {
     id = 190,
     extension = ".LBT",
     format = "FoxPro Label Memo"
 },
 new app_file_type
 {
     id = 191,
     extension = ".HBK",
     format = "Mathcad Handbook File"
 },
 new app_file_type
 {
     id = 192,
     extension = ".TAB",
     format = "Guitar Tablature File"
 },
 new app_file_type
 {
     id = 193,
     extension = ".PWR",
     format = "PowerWrite Document"
 },
 new app_file_type
 {
     id = 194,
     extension = ".PVM",
     format = "Photo Video Manifest File"
 },
 new app_file_type
 {
     id = 195,
     extension = ".BML",
     format = "Braille 2000 Braille File"
 },
 new app_file_type
 {
     id = 196,
     extension = ".JP1",
     format = "Japanese (Romaji) Text File"
 },
 new app_file_type
 {
     id = 197,
     extension = ".SESSION",
     format = "Mozilla Firefox Session File"
 },
 new app_file_type
 {
     id = 198,
     extension = ".MD",
     format = "MuseData Musical Score"
 },
 new app_file_type
 {
     id = 199,
     extension = ".BXT",
     format = "Balabolka Text Document"
 },
 new app_file_type
 {
     id = 200,
     extension = ".WP4",
     format = "WordPerfect 4 Document"
 },
 new app_file_type
 {
     id = 201,
     extension = ".RZN",
     format = "Red Zion Notes File"
 },
 new app_file_type
 {
     id = 202,
     extension = ".MPD",
     format = "MPEG-DASH Media Presentation Description"
 },
 new app_file_type
 {
     id = 203,
     extension = ".NOTES",
     format = "Memento Notes File"
 },
 new app_file_type
 {
     id = 204,
     extension = ".WPD",
     format = "602Text Word Processing Document"
 },
 new app_file_type
 {
     id = 205,
     extension = ".MWP",
     format = "Lotus Word Pro SmartMaster File"
 },
 new app_file_type
 {
     id = 206,
     extension = ".MSS",
     format = "CartoCSS Map Stylesheet"
 },
 new app_file_type
 {
     id = 207,
     extension = ".HIGHLAND",
     format = "Highland Document"
 },
 new app_file_type
 {
     id = 208,
     extension = ".SCRIVX",
     format = "Scrivener XML Document"
 },
 new app_file_type
 {
     id = 209,
     extension = ".HHT",
     format = "Help and Support Center HHT File"
 },
 new app_file_type
 {
     id = 210,
     extension = ".SCRIV",
     format = "Scrivener Document"
 },
 new app_file_type
 {
     id = 211,
     extension = ".MWD",
     format = "Mariner Write Document"
 },
 new app_file_type
 {
     id = 212,
     extension = ".LNK42",
     format = "Windows 93 Desktop Shortcut"
 },
 new app_file_type
 {
     id = 213,
     extension = ".CALCA",
     format = "Calca Document"
 },
 new app_file_type
 {
     id = 214,
     extension = ".SLA.GZ",
     format = "Scribus Compressed Document"
 },
 new app_file_type
 {
     id = 215,
     extension = ".MELLEL",
     format = "Mellel Word Processing Document"
 },
 new app_file_type
 {
     id = 216,
     extension = ".BNA",
     format = "Barna Word Processor Document"
 },
 new app_file_type
 {
     id = 217,
     extension = ".VCF",
     format = "Variant Call Format File"
 },
 new app_file_type
 {
     id = 218,
     extension = ".FDF",
     format = "Acrobat Forms Data Format"
 },
 new app_file_type
 {
     id = 219,
     extension = ".ZRTF",
     format = "Nisus Compressed Rich Text File"
 },
 new app_file_type
 {
     id = 220,
     extension = ".DCA",
     format = "DisplayWrite Document"
 },
 new app_file_type
 {
     id = 221,
     extension = ".MW",
     format = "MacWrite Text Document"
 },
 new app_file_type
 {
     id = 222,
     extension = ".UOT",
     format = "Uniform Office Document"
 },
 new app_file_type
 {
     id = 223,
     extension = ".FLR",
     format = "Flare Decompiled ActionScript File"
 },
 new app_file_type
 {
     id = 224,
     extension = ".BTD",
     format = "Business-in-a-Box Document"
 },
 new app_file_type
 {
     id = 225,
     extension = ".XBDOC",
     format = "Xiosis Scribe Document"
 },
 new app_file_type
 {
     id = 226,
     extension = ".UTXT",
     format = "Unicode Text File"
 },
 new app_file_type
 {
     id = 227,
     extension = ".CNM",
     format = "NoteMap Outline File"
 },
 new app_file_type
 {
     id = 228,
     extension = ".ERR",
     format = "FoxPro Compilation Error"
 },
 new app_file_type
 {
     id = 229,
     extension = "._DOC",
     format = "Renamed Microsoft Word Document"
 },
 new app_file_type
 {
     id = 230,
     extension = ".SAFETEXT",
     format = "SafeText File"
 },
 new app_file_type
 {
     id = 231,
     extension = ".JTD",
     format = "JustSystems Ichitaro Document"
 },
 new app_file_type
 {
     id = 232,
     extension = ".MELL",
     format = "Mellel Word Processing File"
 },
 new app_file_type
 {
     id = 233,
     extension = ".OFL",
     format = "Ots File List"
 },
 new app_file_type
 {
     id = 234,
     extension = ".ACT",
     format = "FoxPro Documenting Wizard Action Diagram"
 },
 new app_file_type
 {
     id = 235,
     extension = ".LYX",
     format = "LyX Document"
 },
 new app_file_type
 {
     id = 236,
     extension = ".WPD",
     format = "ACT! 2 Word Processing Document"
 },
 new app_file_type
 {
     id = 237,
     extension = ".WPL",
     format = "DEC WPS Plus Text Document"
 },
 new app_file_type
 {
     id = 238,
     extension = ".CAST",
     format = "Asciicast Terminal Recording"
 },
 new app_file_type
 {
     id = 239,
     extension = ".EUC",
     format = "Extended Unix Code File"
 },
 new app_file_type
 {
     id = 240,
     extension = ".HZ",
     format = "Chinese (Hanzi) Text"
 },
 new app_file_type
 {
     id = 241,
     extension = ".ORT",
     format = "Rich Text Editor Document"
 },
 new app_file_type
 {
     id = 242,
     extension = ".NJX",
     format = "NJStar Document"
 },
 new app_file_type
 {
     id = 243,
     extension = ".WBK",
     format = "WordPerfect Workbook"
 },
 new app_file_type
 {
     id = 244,
     extension = ".XDL",
     format = "Oracle Expert Definition Language File"
 },
 new app_file_type
 {
     id = 245,
     extension = ".UNAUTH",
     format = "SiteMinder Unauthorized Message File"
 },
 new app_file_type
 {
     id = 246,
     extension = ".FFT",
     format = "Final Form Text File"
 },
 new app_file_type
 {
     id = 247,
     extension = ".WP6",
     format = "WordPerfect 6 Document"
 },
 new app_file_type
 {
     id = 248,
     extension = ".DWD",
     format = "DavkaWriter File"
 },
 new app_file_type
 {
     id = 249,
     extension = ".LWP",
     format = "Lotus Word Pro Document"
 },
 new app_file_type
 {
     id = 250,
     extension = ".PRT",
     format = "Crypt Edit Protected Text Format File"
 },
 new app_file_type
 {
     id = 251,
     extension = ".FWDN",
     format = "fWriter Document"
 },
 new app_file_type
 {
     id = 252,
     extension = ".JOE",
     format = "JOE Document"
 },
 new app_file_type
 {
     id = 253,
     extension = ".RTD",
     format = "RagTime Document"
 },
 new app_file_type
 {
     id = 254,
     extension = ".ZABW",
     format = "Compressed AbiWord Document File"
 },
 new app_file_type
 {
     id = 255,
     extension = ".AWP",
     format = "Ability Write Template"
 },
 new app_file_type
 {
     id = 256,
     extension = ".TMV",
     format = "TextMaker Template"
 },
 new app_file_type
 {
     id = 257,
     extension = ".GPN",
     format = "GlidePlan Map Document"
 },
 new app_file_type
 {
     id = 258,
     extension = ".SAM",
     format = "Ami Pro Document"
 },
 new app_file_type
 {
     id = 259,
     extension = ".TDF",
     format = "Guide Text Definition File"
 },
 new app_file_type
 {
     id = 260,
     extension = ".PMO",
     format = "Pegasus Saved Message File"
 },
 new app_file_type
 {
     id = 261,
     extension = ".AWT",
     format = "AbiWord Template"
 },
 new app_file_type
 {
     id = 262,
     extension = ".XBPLATE",
     format = "Xiosis Scribe Template"
 },
 new app_file_type
 {
     id = 263,
     extension = ".GJAM",
     format = "Google Jamboard Shortcut"
 },
 new app_file_type
 {
     id = 264,
     extension = ".LICENSE",
     format = "Software License File"
 },
 new app_file_type
 {
     id = 265,
     extension = ".PU",
     format = "PlantUML File"
 },
 new app_file_type
 {
     id = 266,
     extension = ".QUID",
     format = "QuidProQuo Document"
 },
 new app_file_type
 {
     id = 267,
     extension = ".GV",
     format = "Graphviz DOT File"
 },
 new app_file_type
 {
     id = 268,
     extension = ".SDW",
     format = "StarOffice Writer Text Document"
 },
 new app_file_type
 {
     id = 269,
     extension = ".SXG",
     format = "Apache OpenOffice Master Document"
 },
 new app_file_type
 {
     id = 270,
     extension = ".OPENBSD",
     format = "OpenBSD Readme File"
 },
 new app_file_type
 {
     id = 271,
     extension = ".PLANTUML",
     format = "PlantUML File"
 },
 new app_file_type
 {
     id = 272,
     extension = ".CRWL",
     format = "Windows Crawl File"
 },
 new app_file_type
 {
     id = 273,
     extension = ".ASCII",
     format = "ASCII Text File"
 },
 new app_file_type
 {
     id = 274,
     extension = ".NB",
     format = "Nota Bene File"
 },
 new app_file_type
 {
     id = 275,
     extension = ".PIMX",
     format = "Adobe Package Installation Management File"
 },
 new app_file_type
 {
     id = 276,
     extension = ".ASE",
     format = "Autodesk ASCII Scene Export File"
 },
 new app_file_type
 {
     id = 277,
     extension = ".VCT",
     format = "Visual Class Library Memo"
 },
 new app_file_type
 {
     id = 278,
     extension = ".XYP",
     format = "XYWrite Plus Document"
 },
 new app_file_type
 {
     id = 279,
     extension = ".XWP",
     format = "Crosstalk Session File"
 },
 new app_file_type
 {
     id = 280,
     extension = ".DEL",
     format = "Delimited ASCII File"
 },
 new app_file_type
 {
     id = 281,
     extension = ".EBP",
     format = "Express Burn Project"
 },
 new app_file_type
 {
     id = 282,
     extension = ".SCT",
     format = "FoxPro Form Memo"
 },
 new app_file_type
 {
     id = 283,
     extension = ".LYT",
     format = "TurboTax Install Log File"
 },
 new app_file_type
 {
     id = 284,
     extension = ".OCR",
     format = "FAXGrapper Fax Text File"
 },
 new app_file_type
 {
     id = 285,
     extension = ".BBS",
     format = "Bulletin Board System Text"
 },
 new app_file_type
 {
     id = 286,
     extension = ".TDF",
     format = "Xserve Test Definition File"
 },
 new app_file_type
 {
     id = 287,
     extension = ".DNE",
     format = "Netica Text File"
 },
 new app_file_type
 {
     id = 288,
     extension = ".WEBDOC",
     format = "Box.net Web Document"
 },
 new app_file_type
 {
     id = 289,
     extension = ".CWS",
     format = "Claris Works Template"
 },
 new app_file_type
 {
     id = 290,
     extension = ".FDXT",
     format = "Final Draft 8 Template"
 },
 new app_file_type
 {
     id = 291,
     extension = ".LNT",
     format = "Laego Note Taker File"
 },
 new app_file_type
 {
     id = 292,
     extension = ".QPF",
     format = "QuickPad Encrypted Document"
 },
 new app_file_type
 {
     id = 293,
     extension = ".SUBLIME-WORKSPACE",
     format = "Sublime Text Workspace File"
 },
 new app_file_type
 {
     id = 294,
     extension = ".GTHR",
     format = "Gather Log File"
 },
 new app_file_type
 {
     id = 295,
     extension = ".UNX",
     format = "Unix Text File"
 },
 new app_file_type
 {
     id = 296,
     extension = ".DESCRIPTION",
     format = "youtube-dl Video Description"
 },
 new app_file_type
 {
     id = 297,
     extension = ".NWP",
     format = "Now Contact WP Document"
 },
 new app_file_type
 {
     id = 298,
     extension = ".WSD",
     format = "WordStar Document"
 },
 new app_file_type
 {
     id = 299,
     extension = ".XDL",
     format = "XML Schema File"
 },
 new app_file_type
 {
     id = 300,
     extension = ".BRX",
     format = "Beam Report Document"
 },
 new app_file_type
 {
     id = 301,
     extension = ".KWD",
     format = "KWord Document"
 },
 new app_file_type
 {
     id = 302,
     extension = ".SKCARD",
     format = "Starfish Sidekick Card File"
 },
 new app_file_type
 {
     id = 303,
     extension = ".DXP",
     format = "Duxbury Print File"
 },
 new app_file_type
 {
     id = 304,
     extension = ".XY3",
     format = "XYWrite III Document"
 },
 new app_file_type
 {
     id = 305,
     extension = ".UOF",
     format = "Uniform Office Document"
 },
 new app_file_type
 {
     id = 306,
     extension = ".PTNX",
     format = "EasyBeadPatterns Pattern"
 },
 new app_file_type
 {
     id = 307,
     extension = ".SW3",
     format = "Scriptware Screenplay"
 },
 new app_file_type
 {
     id = 308,
     extension = ".FGS",
     format = "Fig Figure Settings File"
 },
 new app_file_type
 {
     id = 309,
     extension = ".TID",
     format = "TiddlyWiki Tiddler"
 },
 new app_file_type
 {
     id = 310,
     extension = ".PWDP",
     format = "Password Pad Document"
 },
 new app_file_type
 {
     id = 311,
     extension = ".VPDOC",
     format = "VoodooPad Document"
 },
 new app_file_type
 {
     id = 312,
     extension = ".NWM",
     format = "Nisus Macro"
 },
 new app_file_type
 {
     id = 313,
     extension = ".FDS",
     format = "Final Draft Secure Copy"
 },
 new app_file_type
 {
     id = 314,
     extension = ".CYI",
     format = "Clustify Input File"
 },
 new app_file_type
 {
     id = 315,
     extension = ".PVJ",
     format = "ProofVision Job Ticket"
 },
 new app_file_type
 {
     id = 316,
     extension = ".PFX",
     format = "First Choice Word Processing Document"
 },
 new app_file_type
 {
     id = 317,
     extension = ".EMF",
     format = "Jasspa MicroEmacs Macro File"
 },
 new app_file_type
 {
     id = 318,
     extension = ".WP5",
     format = "WordPerfect 5 Document"
 },
 new app_file_type
 {
     id = 319,
     extension = ".WTX",
     format = "Text Document"
 },
 new app_file_type
 {
     id = 320,
     extension = ".ZW",
     format = "Chinese Text File"
 },
 new app_file_type
 {
     id = 321,
     extension = ".WPA",
     format = "ACT! Word Processing Document"
 },
 new app_file_type
 {
     id = 322,
     extension = ".JRTF",
     format = "JAmes OS Rich Text File"
 },
 new app_file_type
 {
     id = 323,
     extension = ".LTR",
     format = "Letter File"
 },
 new app_file_type
 {
     id = 324,
     extension = ".MIN",
     format = "Mint Source File"
 },
 new app_file_type
 {
     id = 325,
     extension = ".PDPCMD",
     format = "Pdplayer Command File"
 },
 new app_file_type
 {
     id = 326,
     extension = ".WN",
     format = "WriteNow Text Document"
 },
 new app_file_type
 {
     id = 327,
     extension = ".SCW",
     format = "Movie Magic Screenwriter Document"
 },
 new app_file_type
 {
     id = 328,
     extension = ".BZABW",
     format = "Compressed AbiWord Document"
 },
 new app_file_type
 {
     id = 329,
     extension = ".TVJ",
     format = "TrueView Job Ticket"
 },
 new app_file_type
 {
     id = 330,
     extension = ".IIL",
     format = "CleanSweep Installation Log"
 },
 new app_file_type
 {
     id = 331,
     extension = ".ODP",
     format = "Organ Definition Project"
 },
 new app_file_type
 {
     id = 332,
     extension = ".VW",
     format = "Volkswriter Text File"
 },
 new app_file_type
 {
     id = 333,
     extension = ".NWCTXT",
     format = "NoteWorthy Composer Text File"
 },
 new app_file_type
 {
     id = 334,
     extension = ".GMD",
     format = "GroupMail Message"
 },
 new app_file_type
 {
     id = 335,
     extension = ".MCW",
     format = "MacWrite II Document"
 },
 new app_file_type
 {
     id = 336,
     extension = ".DOX",
     format = "MultiMate Document"
 },
 new app_file_type
 {
     id = 337,
     extension = ".THP",
     format = "TurboTax Text String"
 }

                );

            modelBuilder.HasSequence<int>("app_file_type_id")
               .StartsAt(1)
               .IncrementsBy(1);


            modelBuilder.Entity<app_file_type>()
                      .Property(o => o.id)
                      .HasDefaultValueSql("nextval('\"app_file_type_id\"')");

            base.OnModelCreating(modelBuilder);
        }

        private static string GetConnectionString()
        {
            const string databaseName = "webapijwt";
            const string databaseUser = "";
            const string databasePass = "";

            return $"Server=localhost;" +
                   $"database={databaseName};" +
                   $"uid={databaseUser};" +
                   $"pwd={databasePass};" +
                   $"pooling=true;";
        }
    }

}

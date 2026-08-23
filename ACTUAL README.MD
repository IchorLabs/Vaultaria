# **🛠️ Welcome to Vaultaria Development\!**

Welcome to the team\! This document will guide you through setting up your computer to code for the **Vaultaria** Terraria mod.

There are quite a few moving parts to getting a modding environment set up, but if you follow these steps in order, you'll be up and running in no time. Take it slow, and if you ever get stuck or confused, just ask me for help\!

## **1\. Visual Studio Code Setup**

We use Visual Studio Code (VS Code) as our Integrated Development Environment (IDE)—basically, the program where we write all our C\# code.

1. **Use Windows:** Ensure you are developing on a Windows machine. This ensures the best compatibility with our current symlink and build setup.  
2. **Install VS Code:** Download and install it from [code.visualstudio.com](https://code.visualstudio.com/).  
3. **Install Required Extensions:** Open the Extensions tab on the left sidebar (it looks like four squares) or press Ctrl+Shift+X. Search for and install these specific tools:  
   * **C\# Dev Kit** (by Microsoft) \- *Crucial. This highlights errors, provides "IntelliSense" (autocomplete), and understands Terraria's code.*  
   * **Markdown Preview Enhanced** (by Yiyi Wang) \- *Allows you to easily read .md files like this one\! Once installed, press Ctrl+Shift+V to open a formatted preview of this document.*  
4. **Connect GitHub:** In VS Code, look for the Accounts icon in the bottom left corner and sign in to your GitHub account so you have permission to pull and push code.

## **2\. Git & Cloning the Repository**

Before we link the mod to Terraria, we need to download the actual codebase to your computer.

1. Open VS Code.  
2. Press Ctrl+Shift+P to open the Command Palette. Type **Git: Clone** and select it.  
3. Paste the URL for the Vaultaria GitHub repository: https://github.com/IchorLabs/Vaultaria  
4. Choose a safe, dedicated folder on your computer to save it.  
   * *Example:* C:\\Documents\\Coding Projects\\Terraria\\Mods\\Vaultaria  
   * **Important:** Remember this exact folder path; you will need it for the next step.  
5. **Setup your Workspace:**  
   * Once VS Code has opened the codebase, click **File** in the top-left and select Save Workspace as…. Save this file in the root (main) Vaultaria folder.  
   * Then, click **File** \> Open workspace from file… and open that newly created file. This ensures VS Code loads all our specific settings correctly.

## **3\. Git Survival Guide (Version Control)**

Git is how we save our progress and collaborate without overwriting each other's work. You can use the **Source Control tab** on the left side of VS Code (the branch icon) to do this visually, but it is highly recommended to learn the terminal commands.

To open the Terminal in VS Code, press Ctrl \+ \` (the tilde/backtick key above Tab) or go to **Terminal \> New Terminal**.

### **The Golden Rule**

**ALWAYS synchronize/pull changes before you start coding.** If I changed something while you were asleep and you start coding on an old version, our code will conflict\!

### **Essential Git Commands**

* **Checking your status:** Want to see what files you've changed?  
  `git status`

* **Updating your code (Pulling):** Downloads my latest changes from GitHub to your computer. Run this every time you sit down to code.  
  `git pull \--rebase origin main`

* **Creating a safe space to experiment (Branching):**  
  * \-b: creates the new branch  
  * adding-new-sniper: The name of the new main  
  * You don’t have to do this, it’s just something you can do if you want. I personally didn’t do it too much since I trusted myself enough with my own codebase  
    `git checkout \-b adding-new-sniper`

    *(To switch back to main later, type git checkout main)*  
* **Saving your work temporarily (Stashing):** If you started coding on main by accident and want to move those changes to a new branch without losing them:  
  `git stash`                 \# Hides your current changes  
  `git checkout \-b new-idea`  \# Creates and moves to a new branch  
  `git stash pop`             \# Brings your hidden changes back into this new branch

* **Saving your work to GitHub (Commit & Push):** When you are done with a feature and want to upload it:  
  * You can also use the source control button on the left panel instead of using the terminal.  
    `git add .`                             \# Stages ALL your changed files  
    `git commit \-m "Added the new sniper"`  \# Wraps them in a save state with a message  
    `git push origin your-branch-name`      \# Uploads it to GitHub

* **Merging:** Once your branch is pushed and tested, we can merge it into main via GitHub (Pull Requests) or locally:  
  `git checkout main`  
  `git merge your-branch-name`

## **4\. The Symlink (Connecting to Terraria)**

**Why do we do this?** tModLoader expects mod source code to live inside its specific ModSources folder. Instead of moving our Git repository in there (which gets messy with version control), we create a "Junction" (a fake folder). This tricks Terraria into looking directly at our Git folder.

> ⚠️ **CRITICAL RULE:** Do **NOT** manually create a "Vaultaria" folder inside ModSources. The command below will create it for you. If a folder named Vaultaria already exists in ModSources, **delete it first**.

**Step-by-Step:**

1. Open **Command Prompt (CMD)** on your Windows PC.  
2. We are going to use the mklink /J command.  
   * *The syntax is:* mklink /J "\<Path\_To\_Terraria\_ModSources\\Vaultaria\>" "\<Path\_To\_Your\_Cloned\_Repo\>"  
3. **Run the command:** Replace the second path with wherever you cloned the repo in Step 2\.

**Example Command:**

mklink /J "%USERPROFILE%\\Documents\\My Games\\Terraria\\tModLoader\\ModSources\\Vaultaria" "%USERPROFILE%\\Documents\\Coding Projects\\Terraria\\Mods\\Vaultaria"

## **5\. Booting Up the Code**

1. Open VS Code.  
2. Press Ctrl+Shift+P to open the Command Palette, type .NET: Switch Solution and select Vaultaria.sln.  
3. *Note: It may take a minute or two for the solution to fully load and for "IntelliSense" (the autocomplete and error checker) to wake up. Give it a moment to process the Terraria source code when you first boot it up.*

## **6\. Coding Guidelines & Best Practices**

There are A LOT of files in this project\! Be careful when navigating around. Keep these rules in mind:

### **The Basics**

* **Spelling:** I use Australian spelling in most places (e.g., Armoury instead of Armory). It’s ok if you use American spelling, but just keep an eye out for it when searching for files.  
* **Comment Everything:** Add comments everywhere explaining what your code does. I tried to comment all the code that could use explaining. Try to follow the precedent I have set.  
* **Be DRY (Don't Repeat Yourself):** Try to reuse code where possible. Check the Utilities folder for custom functions I've already made (like sound playing or elemental effects). Read the comments in those files to see how to use them\!

### **Architecture & Syntax**

* **Namespaces:** Make sure to create and include correct namespace declarations at the top of every new file you create. Every file should have a namespace that exactly matches the folder path it lives in.  
  * *Example:* namespace Vaultaria.Content.Items.Weapons.Ranged;  
* **Syntax Rules:**  
  * Ensure spacing is set to Tabs (or 4 spaces).  
  * Brackets { } always go on new lines.  
  * Functions and Classes StartWithCapitalLetters.  
* **File Structure:** Try to put all files into scalable folders. My proudest achievement is the weapons folder structure:  
  * Vaultaria \> Content \> Items \> Weapons \> Ranged \> Legendary \> Assault Rifle \> Vladof  
  * Being this categorical allows us to do cool things programmatically, like filtering lists of items based on their folders (which is how the Eridian Fabricator works to only shoot our modded legendary weapons).  
* **Localization:** There is a file called en-US\_Mods.Vaultaria.hjson which contains the names of pretty much everything in the mod.  
  * Whenever any item, bullet, NPC, structure, buff, text or name is added to the mod, it ends up in this file  
  * Just be weary to add a proper label and tooltip if you add a buff so that valid text appears when you hover over the icon in-game

### **Managing Complexity**

* **Split up your code:** As tempting as it is to put all miscellaneous code into one giant Utilities.cs file, you’ll hate yourself later. I did that, it went over 1000 lines, and it was a mess.  
* **"God-Files":** There are a few necessary massive files. The main one is VaultariaPlayer.cs, which contains almost all code related to the player (spawning, dying, buffs, etc.). ElementalGlobalProjectile.cs is another.  
  * *Rule for God-Files:* Keep the main override methods clean\! Put your actual logic into separate, smaller functions, and just *call* those functions inside the main overrides like ModifyHitNPC.  
* **The Sounds Folder:** We have a million .mp3 files in one folder. This is on purpose. There is a master Enum (a list) of all sounds. When you add a new .mp3, add its name to that Enum so our SetItemSound function works effortlessly.

### **When in Doubt**

* There are text files with ideas I haven't implemented yet, and files with huge chunks of commented-out code. For 99% of these: leave them alone, or ask me before playing with them.  
* Hardcoding is sometimes better than making an overly complex, messy inheritance system.  
  * See how I’ve done similar things first.  
* Look at the [tModLoader ExampleMod Github](https://github.com/tModLoader/tModLoader/tree/1.4.4/ExampleMod) for standard ways to do things (like [ExampleSword](https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Items/Weapons/ExampleSword.cs) or [ExampleGun](https://github.com/tModLoader/tModLoader/blob/1.4.4/ExampleMod/Content/Items/Weapons/ExampleGun.cs)).

## **7\. Building & Publishing an Update**

When we are ready to push a new update to the public, follow this exact workflow:

1. **Version Number:** Open build.txt and increase the version number. We use a *Major.Minor.Patch (Mistake)* numbering system (e.g., updating v1.2.4 to v1.2.5).  
2. **Descriptions:** Update both description.txt and description\_workshop.txt to reflect the new changes.  
3. **Publishing:** Boot up tModLoader, go to **Workshop \> Develop Mods**, build Vaultaria, and hit **Publish**.  
4. **Patch Notes:** Be sure to release the patch notes on our Discord server and on the Steam Workshop page\!

## **8\. Useful Modding Links**

Bookmark these; you will need them constantly:

**Documentation & Examples:**

* [tModLoader GitHub Wiki](https://github.com/tModLoader/tModLoader/wiki)  
* [Example Mod Source Code](https://github.com/tModLoader/tModLoader/tree/1.4.4/ExampleMod)

**Vanilla Terraria ID Lists:**

* [Item IDs](https://terraria.fandom.com/wiki/Item_IDs)  
* [Projectile IDs](https://terraria.fandom.com/wiki/Projectile_IDs)  
* [Ammo IDs](https://terraria.wiki.gg/wiki/Ammo_IDs)  
* [Buff IDs](https://terraria.fandom.com/wiki/Buff_IDs)  
* [Sound IDs](https://terraria.wiki.gg/wiki/Sound_IDs)  
* [Dust IDs](https://terraria.fandom.com/wiki/Dust_IDs)  
* [NPC IDs](https://terraria.fandom.com/wiki/NPC_IDs)  
* [Tile IDs](https://terraria.wiki.gg/wiki/Tile_IDs/Part1)

**Tools & Repo:**

* [Vaultaria GitHub Repository](https://github.com/IchorLabs/Vaultaria)  
* [Gifs to Spritesheets Converter](https://ezgif.com/gif-to-sprite)
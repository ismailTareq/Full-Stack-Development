# 🎓 University Research Management System

> A comprehensive database design to help universities keep track of their research activities, researchers, publications, and funding.

---

## 📖 What Is This?

Imagine you're running a university's research department. You need to know:
- Who's doing what research?
- Which projects are active right now?
- Where's the funding coming from?
- Who published what papers?
- Who's supervising whom?

That's **a lot** to keep track of! This database system is designed to organize all of that information in one place, making it easy to find answers to these questions and many more.

---

## 🎯 What Problem Does This Solve?

### The Challenge
Universities handle hundreds of researchers working on dozens of projects, publishing papers, applying for grants, and mentoring students. Without a proper system, information gets scattered across spreadsheets, emails, and filing cabinets. Questions like "How much funding did Project X receive?" or "Which researcher has expertise in AI?" become time-consuming puzzles.

### The Solution
This database acts as a **single source of truth** for all research activities. Everything is connected logically, so you can easily:
- Find all projects a researcher is working on
- See which grants are funding a specific project
- Track who authored which publications
- Monitor supervisory relationships
- Generate reports for administrators and funding agencies

---

## 🏗️ How It's Built

The system is organized around **4 main things** (we call them "entities"):

### 1. 👤 **RESEARCHER** - The People
Think of this as the employee directory for your research department.

**What we track:**
- Basic info (name, email, birthday)
- Office location (which building, which room)
- Contact numbers (office phone, mobile, lab extension)
- What they specialize in (AI, Biology, Chemistry, etc.)
- How old they are (calculated automatically from birthday)

**Example:** Dr. Sarah Chen, age 42, specializes in Machine Learning and Natural Language Processing, office in Building A Room 301.

---

### 2. 📋 **PROJECT** - The Work
Every research project gets its own record.

**What we track:**
- Project title and description keywords
- Timeline (when it starts, when it's expected to finish)
- Budget (how much money is allocated)
- Status (is it active? completed? still pending approval?)
- Who's in charge (the lead researcher)
- How long it's been running (calculated automatically)

**Example:** "AI-Powered Medical Diagnosis System" - Started Jan 2025, $500K budget, Active, led by Dr. Chen, running for 2 months.

---

### 3. 📄 **PUBLICATION** - The Output
When research gets published, we record it here.

**What we track:**
- Publication title
- When it was published
- DOI (like a unique ID for academic papers)
- Where it was published (journal or conference)
- How many times other papers have cited it
- Research keywords
- Complete list of all authors (including people from other universities)

**Example:** "Deep Learning for Cancer Detection" published in Nature Medicine, March 2025, cited 15 times, written by 8 authors from 3 institutions.

---

### 4. 💰 **GRANT** - The Money
Research costs money, and grants provide it.

**What we track:**
- Official grant name
- Total amount awarded
- Grant period (start and end dates)
- Which organization is providing the funding (NSF, NIH, etc.)
- How long the grant lasts (calculated automatically)

**Example:** "National Science Foundation Grant #12345" - $2M awarded, covers 2024-2027, from NSF.

---

## 🔗 How Everything Connects

Here are the **5 key relationships**:

### 1. **SUPERVISES** (Mentor → Student)
Senior researchers mentor junior researchers and doctoral students.

**What we track:**
- Who supervises whom
- When the supervision started
- What type (Mentor, Advisor, or Co-Advisor)

**The Rule:** Each junior researcher can have only ONE primary supervisor, but a senior researcher can supervise MANY people.

**Example:** Professor Smith supervises 5 PhD students; each student has Professor Smith as their only primary advisor.

---

### 2. **LEADS** (Researcher → Project)
Every project needs a boss - someone ultimately responsible.

**What we track:**
- Which researcher leads which project

**The Rule:** Every project MUST have exactly ONE lead researcher, but a researcher can lead MULTIPLE projects.

**Example:** Dr. Chen leads 3 different projects simultaneously.

---

### 3. **WORKS_ON** (Researcher ↔ Project)
Beyond the lead, projects have teams.

**What we track:**
- When someone joined the project
- Their role (Lead Investigator, Co-Investigator, Research Assistant)
- How many hours per week they contribute
- What percentage of project credit they receive

**The Rule:** Researchers can work on multiple projects, and projects can have multiple team members.

**Example:** Dr. Chen works on Project A (20 hrs/week, 40% credit), Project B (10 hrs/week, 25% credit), and Project C (15 hrs/week, 50% credit).

---

### 4. **AUTHORS** (Researcher ↔ Publication)
When our researchers publish papers, we track their contributions.

**What we track:**
- Author position (1st author, 2nd author, etc.)
- Type of contribution (Writing, Methodology, Data Analysis, Review)

**The Rule:** Researchers can author multiple publications, and publications typically have multiple authors from our university.

**Example:** Dr. Chen is 1st author on Paper A (wrote it), 3rd author on Paper B (provided methodology), and 2nd author on Paper C (did data analysis).

---

### 5. **FUNDED_BY** (Grant ↔ Project)
Money flows from grants to projects.

**What we track:**
- How much money from each grant goes to each project
- When the allocation was made

**The Rule:** One grant can fund MULTIPLE projects, and one project can receive money from MULTIPLE grants.

**Example:** Grant #12345 ($2M total) funds Project A ($800K), Project B ($600K), and Project C ($400K). Meanwhile, Project A also receives $200K from Grant #67890.

---

## 📊 Database Structure Summary

**Total: 14 Tables**

### Main Tables (4):
1. **Researcher** - People records
2. **Project** - Research project records
3. **Publication** - Paper records
4. **Grant_Table** - Funding records

### Connection Tables (5):
5. **Supervises** - Links supervisors to students
6. **Works_On** - Links researchers to projects (with role details)
7. **Authors** - Links researchers to publications (with contribution details)
8. **Funded_By** - Links grants to projects (with allocation amounts)
9. **(LEADS is built into Project table as a foreign key)**

### List Tables (5):
10. **Researcher_Phone** - Multiple phone numbers per researcher
11. **Researcher_Area** - Multiple research areas per researcher
12. **Project_Keyword** - Multiple keywords per project
13. **Publication_Keyword** - Multiple keywords per publication
14. **Publication_All_Authors** - Complete author lists per publication

---

## 🎨 Design Philosophy

### Why This Way?

**1. No Repeated Information**
Each fact is stored exactly once. For example, a researcher's email is stored in the Researcher table only - not copied to every project they work on.

**2. Everything Is Connected**
Want to know which grants fund Dr. Chen's projects? Just follow the connections: Dr. Chen → Projects → Funded_By → Grants. Easy!

**3. Automatic Calculations**
Things that can be calculated (like age or project duration) aren't stored - they're computed when needed. This prevents outdated information.

**4. Flexible Relationships**
The system handles complex real-world scenarios:
- A project with 10 team members
- A researcher working on 5 projects
- A grant funding 3 different projects
- A publication with 15 co-authors from 6 universities

**5. Data Quality Rules**
Built-in rules prevent nonsense:
- Can't delete a researcher who's leading active projects
- Every project MUST have at least one funding source
- Can't supervise yourself (that would be weird!)
- Email addresses and DOIs must be unique

---

## 🔍 Real-World Examples

### Example 1: Finding Active Projects
*"What research projects are currently active and who's leading them?"*

The system looks at the **Project** table (filters for status = "Active"), then connects to **Researcher** table through the lead_researcher_id to get the lead's name.

**Answer:** 15 active projects, led by 12 different researchers.

---

### Example 2: Researcher's Track Record
*"Show me everything Dr. Chen has accomplished"*

The system finds Dr. Chen in **Researcher**, then follows connections:
- Through **LEADS**: 3 projects she's leading
- Through **WORKS_ON**: 7 projects she's contributed to
- Through **AUTHORS**: 23 publications she's co-authored
- Through **SUPERVISES**: 5 PhD students she's mentoring

**Answer:** Complete career overview in seconds!

---

### Example 3: Grant Impact
*"Which projects are funded by NSF Grant #12345?"*

Find the grant in **Grant_Table**, follow **FUNDED_BY** connections to **Project** table.

**Answer:** Grant #12345 funds 4 projects totaling $1.8M in allocations.

---

### Example 4: Publication Collaboration
*"Who from our university worked on the cancer detection paper?"*

Find the publication in **Publication**, follow **AUTHORS** connections to **Researcher**.

**Answer:** 3 researchers from our university: Dr. Chen (1st author - writing), Dr. Patel (2nd author - methodology), Dr. Kim (5th author - data analysis).

---

## 📋 Mapping Rules Explained

We followed standard database design rules (ER-to-Relational mapping):

### Rule: **1 M** (One-to-Many)
When one thing connects to many things → Use **2 tables**, add a reference in the "many" side.

**Applied to LEADS:** One researcher leads many projects → Store researcher reference in Project table.

### Rule: **M M** (Many-to-Many)
When many things connect to many things → Use **3 tables** (create a "bridge" table).

**Applied to:**
- **SUPERVISES** → Supervises bridge table
- **WORKS_ON** → Works_On bridge table
- **AUTHORS** → Authors bridge table
- **FUNDED_BY** → Funded_By bridge table

---

## 🚀 Benefits of This System

✅ **Instant Answers** - No more digging through files to answer questions  
✅ **No Duplicate Data** - Each fact stored once, updated once  
✅ **Complete History** - Track everything from start to finish  
✅ **Easy Reporting** - Generate reports for administrators and funding agencies  
✅ **Scalable** - Works for 10 researchers or 1,000  
✅ **Accurate** - Built-in rules prevent data errors  
✅ **Connected** - See relationships between people, projects, and funding  

---

## 💡 Who Uses This?

- **Research Administrators** - Track all active projects and funding
- **Department Heads** - Monitor researcher productivity and grant success
- **Grant Officers** - Manage funding allocations and reporting
- **Researchers** - See their project teams and publication records
- **HR Departments** - Maintain accurate researcher profiles
- **University Leadership** - Generate strategic research reports

---

## 🎓 Technical Details

**Database Type:** Relational (SQL)  
**Total Tables:** 14  
**Design Standard:** ER-to-Relational Mapping Rules  
**Notation:** Standard (1, M) cardinality  
**Normalization:** 3rd Normal Form (3NF)  
**Integrity:** Foreign keys, check constraints, NOT NULL constraints  

---

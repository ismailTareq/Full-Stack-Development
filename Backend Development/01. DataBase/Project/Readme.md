# University Research Management System - Database Design Guide

## Overview

This document explains how we've designed a database to help a university manage its research activities. Think of it as a digital filing system that keeps track of researchers, their projects, publications, and funding sources—all connected in a way that makes sense.

---

## What We're Tracking (The Main Entities)

### 1. **Researchers** 
The people doing the work—faculty members, research staff, and doctoral students. We store their basic info like names, contact details, office locations, and what fields they specialize in.

### 2. **Projects**
The actual research work being done. Each project has a title, timeline, budget, and current status (is it active, completed, or just pending?).

### 3. **Publications**
The research papers and articles that come out of all this work. We track where they're published (journals or conferences), citation counts, and who wrote them.

### 4. **Grants**
The money that makes everything possible. Each grant comes from a funding agency and has a specific amount and time period.

---

## How Everything Connects (The Relationships)

### 1. **Supervision (Researcher ↔ Researcher)**
Senior researchers mentor junior ones. It's like an academic family tree where:
- A senior researcher can supervise many junior researchers
- But each junior researcher typically has just one primary supervisor
- We track when the supervision started and what type it is (mentor, advisor, or co-advisor)

### 2. **Project Leadership (Researcher → Project)**
Every project needs a boss—someone who's ultimately responsible. We call this the "lead researcher" or principal investigator:
- One researcher can lead multiple projects
- But each project has exactly one lead researcher

### 3. **Project Participation (Researcher ↔ Project)**
Beyond the lead, projects often have teams. This tracks who's working on what:
- We record when someone joined
- What their role is (Lead Investigator, Co-Investigator, Research Assistant)
- How many hours per week they're putting in
- What percentage of credit they get

### 4. **Authorship (Researcher ↔ Publication)**
When our university researchers publish papers, we track:
- Their position in the author list (first author, second author, etc.)
- What they contributed (writing, methodology, data analysis, review)
- Note: Publications might have authors from other institutions too—we keep a complete list

### 5. **Funding (Grant ↔ Project)**
Money flows from grants to projects. It's many-to-many because:
- One grant can fund several projects
- One project might get money from multiple grants
- We track exactly how much money goes where and when

---

## The Database Structure

### Researcher Information

**Main Table:**
```sql
Researcher
├── researcher_id (unique identifier)
├── email
├── date_of_birth
├── first_name, middle_name, last_name
├── building_name, room_number (office location)
└── age (automatically calculated from birth date)
```

**Supporting Tables:**
- **Researcher_Phone**: Multiple phone numbers (office, mobile, lab)
- **Researcher_Area**: Areas of expertise (AI, Machine Learning, Database Systems, etc.)

### Project Information

**Main Table:**
```sql
Project
├── project_id (unique identifier)
├── title
├── start_date, end_date
├── budget
├── status (Active, Completed, or Pending)
├── lead_researcher_id (who's in charge)
└── duration_months (automatically calculated)
```

**Supporting Tables:**
- **Project_Keyword**: Research keywords that describe the project

**Important Note:** You might wonder where we store which funding agencies support a project. We don't store that directly—instead, we can find it by looking at which grants fund the project, and each grant tells us its funding agency. This prevents us from storing the same information twice.

### Publication Information

**Main Table:**
```sql
Publication
├── publication_id (unique identifier)
├── title
├── publication_date
├── doi (Digital Object Identifier - like a paper's social security number)
├── type (Journal or Conference)
└── citation_count (how many times it's been cited)
```

**Supporting Tables:**
- **Publication_Keyword**: Keywords for searching
- **Publication_All_Authors**: Complete list of all authors (including those from other universities)

### Grant Information

**Main Table:**
```sql
Grant
├── grant_id (unique identifier)
├── grant_name
├── amount (total funding)
├── start_date, end_date
├── funding_agency (who's providing the money)
└── grant_period_months (automatically calculated)
```

### Connection Tables (How Things Relate)

**Supervises** - Links supervisors to their students:
- supervisor_id, supervisee_id
- start_date, supervision_type
- Prevents someone from supervising themselves (that would be weird!)

**Works_On** - Tracks who's working on which projects:
- researcher_id, project_id
- join_date, role, hours_per_week, credit_percentage

**Authors** - Links our researchers to their publications:
- researcher_id, publication_id
- author_position (1st, 2nd, 3rd, etc.)
- contribution_type (what they did)

**Funded_By** - Shows which grants fund which projects:
- grant_id, project_id
- allocated_amount (how much of the grant goes to this project)
- allocation_date (when this was decided)

---

## Important Design Choices We Made

### Why Track the Project Lead Separately?

Every project must have a lead researcher—someone who's ultimately accountable. Rather than making a separate table for this simple relationship, we just store the lead_researcher_id right in the Project table. It's cleaner and makes queries faster.

However, we also put the lead researcher in the Works_On table with everyone else. Why? Because we want to track their hours and contribution percentage too, just like other team members.

### Why Two Ways to Track Authors?

You might notice we track authors in two places:
1. **Publication_All_Authors**: A simple list of everyone who wrote the paper (including people from other universities)
2. **Authors table**: Detailed info only about our university's researchers (their position, what they contributed)

This dual approach lets us maintain complete author lists while also capturing detailed contribution data for our own researchers.

### How Do We Handle "Many-to-Many" Relationships?

Some relationships are complex. For example:
- A researcher can work on many projects
- A project can have many researchers

We can't just add a column to handle this—we need a whole separate table (Works_On) that acts as a bridge, storing all the details about each researcher-project combination.

### What Gets Calculated vs. Stored?

Some information doesn't need to be stored—we can calculate it when needed:
- **Age**: Calculated from date of birth
- **Project duration**: Calculated from start and end dates
- **Grant period**: Calculated from start and end dates

This prevents outdated information (imagine if we stored someone's age and forgot to update it every year!).

---

## Real-World Examples

### Example 1: Finding All Active Projects and Their Leaders
```sql
SELECT 
    p.title AS "Project Name",
    CONCAT(r.first_name, ' ', r.last_name) AS "Lead Researcher",
    p.budget AS "Budget",
    TIMESTAMPDIFF(MONTH, p.start_date, CURDATE()) AS "Months Running"
FROM Project p
JOIN Researcher r ON p.lead_researcher_id = r.researcher_id
WHERE p.status = 'Active';
```

This gives us a nice list of what's currently happening in the university's research programs.

### Example 2: Who's Supervising Whom?
```sql
SELECT 
    CONCAT(senior.first_name, ' ', senior.last_name) AS "Supervisor",
    CONCAT(junior.first_name, ' ', junior.last_name) AS "Student",
    s.supervision_type AS "Type",
    s.start_date AS "Started"
FROM Supervises s
JOIN Researcher senior ON s.supervisor_id = senior.researcher_id
JOIN Researcher junior ON s.supervisee_id = junior.researcher_id
ORDER BY senior.last_name;
```

This shows the mentorship structure—who's guiding whom.

### Example 3: Where's the Money Coming From?
```sql
SELECT 
    p.title AS "Project",
    g.funding_agency AS "Funded By",
    fb.allocated_amount AS "Amount",
    fb.allocation_date AS "Date Allocated"
FROM Project p
JOIN Funded_By fb ON p.project_id = fb.project_id
JOIN Grant_Table g ON fb.grant_id = g.grant_id
WHERE p.project_id = 1001;
```

This shows all the funding sources for a specific project—crucial for financial reporting.

### Example 4: A Researcher's Publication Track Record
```sql
SELECT 
    pub.title AS "Paper Title",
    pub.publication_date AS "Published",
    pub.type AS "Type",
    a.author_position AS "Author Position",
    pub.citation_count AS "Citations"
FROM Publication pub
JOIN Authors a ON pub.publication_id = a.publication_id
WHERE a.researcher_id = 1001
ORDER BY pub.publication_date DESC;
```

Great for performance reviews or grant applications!

---

## Rules We've Built In

### Data Integrity Rules

1. **Can't delete a researcher who's leading active projects** - That would leave projects leaderless!

2. **Every project must have at least one grant** - No money, no research!

3. **Every publication must have at least one author from our university** - Otherwise, why are we tracking it?

4. **Can't supervise yourself** - We have a check to prevent this logical impossibility.

5. **Email addresses and DOIs must be unique** - No duplicates allowed.

### Participation Rules

**Optional (you don't have to):**
- Researchers don't have to supervise anyone
- Researchers don't have to work on projects or publish papers
- Grants don't have to fund projects (maybe they're brand new)

**Required (you must):**
- Every project must have exactly one lead researcher
- Every project must receive funding from at least one grant
- Every publication must have at least one university author

## Summary

We've designed a database with **14 tables** that work together:
- **4 main tables** for entities (Researchers, Projects, Publications, Grants)
- **5 tables** for handling lists (phone numbers, research areas, keywords, etc.)
- **5 tables** for handling relationships (supervision, authorship, funding, etc.)

The design follows database best practices:
- No duplicate information
- Everything is properly connected
- Rules enforce data quality
- Queries can efficiently answer real questions

Most importantly, it matches how research really works at a university—capturing the complexity while keeping things manageable!

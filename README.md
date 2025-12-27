# AssetWorks Integration Feasibility Demo  
**Bi-directional Recall Management Integration (ASP.NET 8)**

## Overview

This repository contains a **feasibility demo** for a bi-directional integration between a recalls management platform and **AssetWorks-style fleet and asset management systems**.

The purpose of this project is **technical discovery**, not full production delivery. It is designed to validate whether and how data can flow in both directions while allowing AssetWorks to remain the **system of record** for fleet and asset data.

This mirrors the type of early-stage integration assessment typically done before committing to a full build.

---

## Problem Context

AssetWorks is commonly used by fleets as the authoritative platform for:
- Vehicles and equipment
- VINs and unit identifiers
- Maintenance and operational status

Recall management systems, however, need to:
- Consume accurate asset and VIN data
- Track recall campaigns independently
- Reflect recall completion or status back to fleet operations

The core challenge is enabling this **without duplicating ownership**, creating data conflicts, or violating system-of-record boundaries.

---

## What This Demo Shows

This demo models a realistic and low-risk integration approach that supports:

### Asset Ingestion (AssetWorks → Recalls Platform)
- Pulling asset and fleet data from an AssetWorks-style API
- VIN-based asset identification
- Incremental sync using timestamps
- AssetWorks treated as read-authoritative

### Independent Recall Management
- Recalls are managed outside AssetWorks
- Recalls are linked to assets via VIN
- Recall severity, campaign details, and status are owned by the recall platform

### Controlled Push-Back of Recall Updates
- Recall updates are queued internally
- Updates are pushed back through a dedicated integration boundary
- No direct mutation of core asset records
- Designed to map to common AssetWorks patterns such as notes, work orders, or status markers, depending on API capabilities

### Feasibility-First Architecture
- Single ASP.NET Core (.NET 8) project
- Clean Architecture principles using **folders only**
- Clear separation between domain, use cases, infrastructure, and API
- Makes constraints and assumptions explicit and easy to review

---

## Architecture Overview

The solution is intentionally kept as a **single project** for clarity during discovery.


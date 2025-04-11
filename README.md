# API Documentation



## Table of Contents
- [Auth](#auth-endpoints)
- [User](#user-endpoints)
- [Admin](#admin-endpoints)
- [Roles](#roles-endpoints)
- [Pharmacy](#pharmacy-endpoints)
- [Items](#items-endpoints)
- [Article](#article-endpoints)
- [Cart](#cart-endpoints)

---

## Auth Endpoints
No Token need
| Method | Endpoint                          | Description                          |
|--------|-----------------------------------|--------------------------------------|
| POST   | `/auth/register`                  | Register a new user                  |
| POST   | `/auth/login`                     | User login                           |
| POST   | `/auth/confirm-email`             | Confirm email address                |
| POST   | `/auth/resend-configration-email` | Resend confirmation email            |
| POST   | `/auth/refresh-token`             | Refresh access token                 |
| POST   | `/auth/revoke-refresh-token`      | Revoke refresh token                 |
| POST   | `/auth/forget-password`           | Request password reset               |
| POST   | `/auth/reset-password`            | Reset user password                  |

---

## User Endpoints
Token need User Or Admin
| Method | Endpoint                   | Description                          |
|--------|----------------------------|--------------------------------------|
| GET    | `/me`                      | Get current user info                |
| PUT    | `/me/info`                 | Update user information              |
| PUT    | `/me/change-password`      | Change user password                 |
| POST   | `/me/upload-image`         | Upload user profile image            |
| GET    | `/me/image-stream`         | Get user profile image               |

---

## Admin Endpoints
Token need Admin only
| Method | Endpoint                          | Description                          |
|--------|-----------------------------------|--------------------------------------|
| GET    | `/admin`                          | Get all Users                        |
| POST   | `/admin`                          | Create new User                      |
| GET    | `/admin/{id}`                     | Get User by ID                       |
| PUT    | `/admin/{userid}`                 | Update User =>Role                   |
| PUT    | `/admin/toggle-status/{userid}`   | Toggle user status                   |
| PUT    | `/admin/unlock-user/{userid}`     | Unlock user account                  |

---

## Roles Endpoints
Token need Admin only
| Method | Endpoint                          | Description                          |
|--------|-----------------------------------|--------------------------------------|
| GET    | `/roles`                          | Get all roles                        |
| POST   | `/roles`                          | Create new role                      |
| PUT    | `/roles/{roleid}`                 | Update role                          |
| PUT    | `/roles/toggle-status/{roleid}`   | Toggle role status                   |

---

## Pharmacy Endpoints
read endpoints Take User and Admin Token
write endpoints Take admin Token only
| Method | Endpoint                   | Description                          |
|--------|----------------------------|--------------------------------------|
| GET    | `/pharmacy`                | Get all pharmacies                   |
| POST   | `/pharmacy`                | Create new pharmacy                  |
| GET    | `/pharmacy/by-id/{id}`     | Get pharmacy by ID                   |
| GET    | `/pharmacy/by-name/{name}` | Get pharmacy by name                 |
| PUT    | `/pharmacy/{id}`           | Update pharmacy                      |

---

## Items Endpoints
read endpoints Take User and Admin Token
write endpoints Take admin Token only
| Method | Endpoint                                      | Description                          |
|--------|-----------------------------------------------|--------------------------------------|
| GET    | `/pharmacy/{pharmacyid}/items`                | Get all items in pharmacy            |
| POST   | `/pharmacy/{pharmacyid}/items`                | Create new item in pharmacy          |
| GET    | `/pharmacy/{pharmacyid}/items/care`           | Get care items in pharmacy           |
| GET    | `/pharmacy/{pharmacyid}/items/medicine`       | Get medicine items in pharmacy       |
| GET    | `/pharmacy/{pharmacyid}/items/by-id/{id}`     | Get item by ID                       |
| GET    | `/pharmacy/{pharmacyid}/items/by-name/{name}` | Get item by name                     |
| PUT    | `/pharmacy/{pharmacyid}/items/{itemid}`       | Update item                          |

---
read endpoints Take User and Admin Token
write endpoints Take admin Token only
## Article Endpoints
| Method | Endpoint                   | Description                          |
|--------|----------------------------|--------------------------------------|
| GET    | `/article`                 | Get all articles                     |
| POST   | `/article`                 | Create new article                   |
| GET    | `/article/by-id/{id}`      | Get article by ID                    |
| GET    | `/article/by-name/{name}`  | Get article by name                  |
| PUT    | `/article/{id}`            | Update article                       |

---
User Token Only
## Cart Endpoints
| Method | Endpoint           | Description                          |
|--------|--------------------|--------------------------------------|
| POST   | `/cart`            | Create cart                          |
| GET    | `/cart`            | Get cart contents                    |
| PUT    | `/cart`            | clear cart                           |
| POST   | `/cart/add-item`   | Add item to cart                     |

# User
## Create User

**URL** : `​/api/User`

**Method** : `Post`

**Auth required** : NO

### Request

```json
{
  "username": "string",
  "email": "user@example.com",
  "password": "string",
  "picture": "string"
}
```
### Responses

**Condition** : New Username/email inserted

**Code** : `200 OK`

**Condition** : Username already exists

**Code** : `400 Username already exists`

**Condition** : Email already exists

**Code** : `400 Email already exists`

## Edit User

Change Username or profile picture or both

**URL** : `​/api/User`

**Method** : `PUT`

**Auth required** : YES

### Request

```json
{
  "username": "string",
  "picture": "string"
}
```
### Responses

**Condition** : New Username

**Code** : `200 OK`

**Condition** : Username already exists

**Code** : `400 Username already exists`

## SingIn User

**URL** : `​/api​/User​/signIn`

**Method** : `Post`

**Auth required** : NO

### Request

**Content** : Sign In can be done by either username or password
```json
{
  "userEmail": "string",
  "password": "string"
}
```
### Responses

**Condition** : Valid username and password

**Code** : `200 OK`

**Content** : jwt token

{
  "token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InN0cmluZyIsIm5iZiI6MTYxMTgyODgwNiwiZXhwIjoxNjExODQ2ODA2fQ.LYl1SmWvemJlY0SPTw9OuKCEmGnrS33sWtE5LGgWs-KS8F5zAiX50TmfNpP2EmNOAzRHrXO_trLk8FmwUDmGQw",
  "expireIn": 18000,
  "succeeded": true
}

**Condition** : Invalid Email/Username

**Code** : `400 Invalid Email/Username`

**Condition** : Incorrect password

**Code** : `400 Incorrect password`

## Search user by username

Show list of users with searched keyword in their username

**URL** : `/api/User/username/{username}`

**Method** : `GET`

**Auth required** : NO

### Request

`/api/User/username/t`

### Success Responses

**Code** : `200 OK`

```json
[
  {
    "id": 4,
    "username": "tom",
    "email": "user4@example.com",
    "picture": "string"
  },
  {
    "id": 5,
    "username": "string",
    "email": "user@example.com",
    "picture": "string"
  }
]

## Search user by id

**URL** : `​/api​/User​/{id}`

**Method** : `Get`

**Auth required** : NO

### Request

`/api/User/5`

### Responses

**Condition** : User id exits

**Code** : `200 OK`

```json
{
  "id": 5,
  "username": "string",
  "email": "user@example.com",
  "picture": "string"
}
```

**Condition** : User id does not exit

**Code** : `400 Invalid user id`

## Follow User

**URL** : `/api/User/follow`

**Method** : `POST`

**Auth required** : YES

### Request

```json
{
  "id": 5,
  "username": "string"
}
```

### Responses

**Condition** : User exits

**Code** : `200 OK`

**Condition** : User does not exit

**Code** : `400 Invalid user`

## Unfollow User

**URL** : `/api/User/unfollow`

**Method** : `POST`

**Auth required** : YES

### Request

```json
{
  "id": 5,
  "username": "string"
}
```

### Responses

**Condition** : User exits

**Code** : `200 OK`

**Condition** : User does not exit

**Code** : `400 Invalid user`
# Info
## Show Current Top 10 Used Hashtags

**URL** : `/api/Info/TopHashtags`

**Method** : `GET`

**Auth required** : NO

### Success Responses

**Code** : `200 OK`

**Content** : An array of hashtags will be returned , containing hashtag id , content and usage count sorted by usage count in discending order

```json
[
  {
    "id": 13,
    "content": "tag3",
    "usageCount": 6
  },
  {
    "id": 14,
    "content": "tag4",
    "usageCount": 5
  },
  {
    "id": 15,
    "content": "tag5",
    "usageCount": 4
  },
  {
    "id": 16,
    "content": "tag6",
    "usageCount": 4
  },
  {
    "id": 17,
    "content": "tag7",
    "usageCount": 3
  },
  {
    "id": 18,
    "content": "tag8",
    "usageCount": 3
  },
  {
    "id": 19,
    "content": "tag9",
    "usageCount": 3
  },
  {
    "id": 20,
    "content": "tag10",
    "usageCount": 3
  },
  {
    "id": 21,
    "content": "tag11",
    "usageCount": 2
  },
  {
    "id": 22,
    "content": "tag12",
    "usageCount": 2
  }
]
```
## Show User's recent activity log
Show user's recent activity log .Activty May include retweeting. liking tweets or following another user

**URL** : `/api/Info/SelfLogs`

**Method** : `GET`

**Auth required** : YES

### Success Responses

**Code** : `200 OK`

**Content** : An array of activity logs , user will be the actor 

```json
[
    {
        "id": 1,
        "actorId": 1,
        "actorName": "user1",
        "actionTypeName": "Follow",
        "actionTypeId": 3,
        "targetTweetId": -1,
        "targetUserId": 4,
        "date": "2021-01-28T11:08:03.3193899"
    }
]
```
## Show recent activity happened to user
Show recent activity happened to user by other users

**URL** : `/api/Info/HomeLogs`

**Method** : `GET`

**Auth required** : YES

### Success Responses

**Code** : `200 OK`

**Content** : An array of activity logs , user will be the target and in case of like/retweet the user's tweet will also be the target 

```json
[
    {
        "id": 1,
        "actorId": 1,
        "actorName": "user1",
        "actionTypeName": "Follow",
        "actionTypeId": 3,
        "targetTweetId": -1,
        "targetUserId": 4,
        "date": "2021-01-28T11:08:03.3193899"
    }
]
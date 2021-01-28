# Tweet
## Create Tweet

**URL** : `​/api​/Tweets`

**Method** : `Post`

**Auth required** : YES

### Request
**Content** :  tweet content , hashtags = array of hashtag content

```json
{
  "content": "string",
  "hashTags": [
    {
      "content": "string"
    }
  ]
}
```
### Success Responses

**Code** : `200 OK`

## Delete Tweet

**URL** : `/api/Tweet/{id}`

**Method** : `Delete`

**Auth required** : YES

### Responses

**Condition** : Tweet id exits

**Code** : `200 OK`

**Condition** : Tweet id does not exit

**Code** : `400 Invalid tweet id`

## Get Tweet By id

**URL** : `/api/Tweet/{id}`

**Method** : `Get`

**Auth required** : NO

### Responses

**Condition** : Tweet id exits

**Code** : `200 OK`

```json
{
    "id": 9,
    "content": "This is my second tweet",
    "createdAt": "2021-01-27T18:29:54.9386105",
    "creatorId": 2,
    "likeCount": 2,
    "retweetCount": 0,
    "isRetweet": false
}
```

**Condition** : Tweet id does not exit

**Code** : `400 Invalid tweet id`

## Like a Tweet

**URL** : `/api/Tweet/like/{id}`

**Method** : `Get`

**Auth required** : YES

### Responses

**Condition** : Tweet id exits

**Code** : `200 OK`

**Condition** : Tweet id does not exit

**Code** : `400 Invalid tweet id`

## Get Tweet Likers
Get List of users that liked a tweet

**URL** : `/api/Tweet/like/likers/{id}`

**Method** : `Get`

**Auth required** : YES

### Responses

**Condition** : Tweet id exits

**Code** : `200 OK`

**Content** : An array of users that liked the tweet

```json
[
    {
        "id": 2,
        "username": "user2",
        "email": "user2@example.com",
        "picture": null
    },
    {
        "id": 3,
        "username": "user2",
        "email": "user3@example.com",
        "picture": null
    }
]
```

**Condition** : Tweet id does not exit

**Code** : `400 Invalid tweet id`

## Retweet a Tweet

**URL** : `/api/Tweet/retweet/{id}`

**Method** : `Get`

**Auth required** : YES

### Responses

**Condition** : Tweet id exits

**Code** : `200 OK`

**Condition** : Tweet id does not exit

**Code** : `400 Invalid tweet id`

## Show Home Tweets
Show user's followings' recent tweets

**URL** : `​/api​/Tweet​/HomeTweets`

**Method** : `Get`

**Auth required** : YES

### Responses

**Code** : `200 OK`

**Content** : list of recent tweets

```json
[
    {
        "id": 9,
        "content": "This is my second tweet",
        "createdAt": "2021-01-27T18:29:54.9386105",
        "creatorId": 2,
        "likeCount": 2,
        "retweetCount": 0,
        "isRetweet": false
    }
]
```

## Show Self Tweets
Show user's own tweets

**URL** : `​/api/Tweet/SelfTweets`

**Method** : `Get`

**Auth required** : YES

### Responses

**Code** : `200 OK`

**Content** : list of tweets

```json
[
    
    {
        "id": 11,
        "content": "tweet",
        "createdAt": "2021-01-27T22:18:19.7942841",
        "creatorId": 1,
        "likeCount": 0,
        "retweetCount": 0,
        "isRetweet": false
    },
    {
        "id": 10,
        "content": "string",
        "createdAt": "2021-01-27T22:14:10.2966872",
        "creatorId": 1,
        "likeCount": 0,
        "retweetCount": 0,
        "isRetweet": false
    },
    {
        "id": 2,
        "content": "My Second Tweet",
        "createdAt": "2021-01-27T15:38:29.5946271",
        "creatorId": 1,
        "likeCount": 0,
        "retweetCount": 0,
        "isRetweet": false
    },
    {
        "id": 1,
        "content": "My First Tweet",
        "createdAt": "2021-01-27T15:17:13.6313635",
        "creatorId": 1,
        "likeCount": 0,
        "retweetCount": 0,
        "isRetweet": false
    }
]
```
## Search For Tweet by Content

Show list of tweets containing the search word/phrase/sentence

**URL** : `​/api/Tweet/SearchByContent`

**Method** : `Post`

**Auth required** : NO

### Request
**Content** :  string containing searched content

```json
{
  "content": "tweet"
}
```
### Success Responses

**Code** : `200 OK`

```json
[
  {
    "id": 9,
    "content": "This is my second tweet",
    "createdAt": "2021-01-27T18:29:54.9386105",
    "creatorId": 2,
    "likeCount": 2,
    "retweetCount": 0,
    "isRetweet": false
  },
  {
    "id": 2,
    "content": "My Second Tweet",
    "createdAt": "2021-01-27T15:38:29.5946271",
    "creatorId": 1,
    "likeCount": 0,
    "retweetCount": 0,
    "isRetweet": false
  },
  {
    "id": 1,
    "content": "My First Tweet",
    "createdAt": "2021-01-27T15:17:13.6313635",
    "creatorId": 1,
    "likeCount": 0,
    "retweetCount": 0,
    "isRetweet": false
  }
]
```

## Search For Tweet by Hashtag

Show list of tweets the searched hashtag has been used in

**URL** : `​/api/Tweet/SearchByHashtag`

**Method** : `Post`

**Auth required** : NO

### Request
**Content** :  string containing searched hastag

```json
{
  "content": "tag"
}
```
### Success Responses

**Code** : `200 OK`

```json
[
    {
    "id": 11,
    "content": "hastag test",
    "createdAt": "2021-01-27T22:18:19.7942841",
    "creatorId": 1,
    "likeCount": 0,
    "retweetCount": 0,
    "isRetweet": false
  },
  {
    "id": 9,
    "content": "This is my second tweet",
    "createdAt": "2021-01-27T18:29:54.9386105",
    "creatorId": 2,
    "likeCount": 2,
    "retweetCount": 0,
    "isRetweet": false
  },
  {
    "id": 1,
    "content": "My First Tweet",
    "createdAt": "2021-01-27T15:17:13.6313635",
    "creatorId": 1,
    "likeCount": 0,
    "retweetCount": 0,
    "isRetweet": false
  }
]
```
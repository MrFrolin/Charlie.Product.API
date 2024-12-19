# Product API Documentation

## Endpoints

### 1. **Get All Products**

- **URL:** `/api/product`
- **Method:** `GET`
- **Description:** This endpoint retrieves all products.
- **Response:**
  - **Status Code:** `200 OK`
  - **Body:** 
    ```json
    {
      "message": "products"
    }
    ```

### 2. **Get Product by ID**

- **URL:** `/api/product/{id}`
- **Method:** `GET`
- **Description:** This endpoint retrieves a product by its unique ID.
- **URL Parameters:**
  - `id` (integer): The unique identifier of the product.
- **Request Example:**
  ```bash
  GET /api/product/1

### 3. **Add a New Product**

- **URL:** `/api/product`
- **Method:** `POST`
- **Description:** This endpoint allows you to create a new product.
- **Request Body:** The body must contain the product data in JSON format. The product must include an `Id`, `Name`, `Description`, and `Price`.
  - **Example Request Body:**
    ```json
    {
      "Id": 1,
      "Name": "Product Name",
      "Description": "Product Description",
      "Price": 100.0
    }
    ```

- **Response:**
  - **Status Code:** `202 Accepted` if the product creation operation is successfully started.
  - **Response Body:**
    ```json
    {
      "Message": "Product creation started.",
      "CorrelationId": "string-guid",
      "Payload": {
        "Id": 1,
        "Name": "Product Name",
        "Description": "Product Description",
        "Price": 100.0
      }
    }
    ```

  - **Status Code:** `400 Bad Request` if the `Id` of the product is missing or invalid in the request body.
  - **Response Body:**
    ```json
    {
      "message": "Invalid product data."
    }
    ```

---

### Message Structure Sent to RabbitMQ:

- **CorrelationId**: A unique identifier for tracking the request.
- **Operation**: "Create" to indicate a product creation operation.
- **Payload**: Contains the product data being sent to RabbitMQ.

Example Message sent to RabbitMQ:
```json
{
  "CorrelationId": "string-guid",
  "Operation": "Create",
  "Payload": {
    "Id": 1,
    "Name": "Product Name",
    "Description": "Product Description",
    "Price": 100.0
  }
}

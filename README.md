# Product API Documentation

## Base URL

The base URL for all the endpoints is:

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

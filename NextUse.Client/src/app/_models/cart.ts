export interface CartItemResponse {
  id: number;
  productId: number;
  title: string;
  unitPrice: number;
  quantity: number;
}

export interface CartResponse {
  id: number;
  status: string;      
  createdAt: string;   
  updatedAt: string;   
  total: number;
  items: CartItemResponse[];
}

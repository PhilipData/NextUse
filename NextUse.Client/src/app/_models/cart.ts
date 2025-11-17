import { Product } from "./product";
import { Profile } from "./profile";

export interface Cart {
  id: number;
  profileId: number;
  profile: Profile;
  anonymousId?: string | null;
  status: string;
  createdAt: string;   
  updatedAt: string; 
  items: CartItem[];
}

export interface CartItem {
  id: number;
  cartId: number;
  productId: number;
  product: Product;
  quantity: number;
  unitPrice: number;
}

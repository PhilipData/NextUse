import { Profile } from "./profile";
import { Product } from "./product";

export interface UserComment {
    id: number;
    content: string;
    createdAt: string;
    profileId?: number | null;
    profile?: Profile | null;
    productId?: number | null;
    product?: Product | null;
}
import { Address } from "./address";
import { Category } from "./category";
import { Profile } from "./profile";
import { UserComment } from "./comment";
import { Bookmark } from "./bookmark";
import { Image } from "./image";

export interface Product {
  id: number;
  title: string;
  description: string;
  price: number;

  addressId: number;
  address?: Address | null;  // optional because it's nullable in C#

  categoryId?: number | null;
  category?: Category | null;  // optional because it's nullable in C#

  profileId: number;
  profile?: Profile | null;

  comments: UserComment[];
  bookmarks: Bookmark[];
  images: Image[];
}

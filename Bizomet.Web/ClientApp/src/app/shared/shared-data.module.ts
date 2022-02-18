import { UserRole } from "./models/user-role.model";

export class SharedData {
  public static all_roles: UserRole[] = [
    { key: "Talent", name: "Talent" },
    { key: "Uplifter", name: "Uplifter" },
    { key: "MediaAssistant", name: "Media Assistant" },
    { key: "Promoter", name: "Promoter" },
    { key: "Producer", name: "Producer" }
  ];
}
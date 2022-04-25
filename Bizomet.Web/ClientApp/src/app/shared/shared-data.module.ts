import { UserRole } from "./models/user-role.model";
import { LookupModel } from './models/lookup.model';

export class SharedData {
  public static all_roles: UserRole[] = [
    { key: "Talent", name: "Talent" },
    { key: "Uplifter", name: "Uplifter" },
    { key: "MediaAssistant", name: "Media Assistant" },
    { key: "Promoter", name: "Promoter" },
    { key: "Producer", name: "Producer" }
  ];

  public static interview_conditions: LookupModel[] = [
    { code: 'FREE_INTERVIEW', icon: "", title: "No Budget", description: 'I will do an interview for free' },
    { code: 'I_CHARGE_INTERVIEW', icon: "", title: "Charge", description: 'I will charge money for my interview' },
    { code: 'I_PAY_INTERVIEW', icon: "", title: "Pay", description: 'I will pay if someone interviews me' },
    { code: 'I_COLOBORATE_FREE_INTERVIEW', icon: "", title: "Collaborate", description: 'I will collaborate free for an exchange interview' },
    { code: 'OTHER', icon: "", title: "Other", description: 'Other' }
  ];

  public static interview_results: Map<string, LookupModel> = new Map<string, LookupModel>([
    ['TEXT_PUBLICATION', { code: 'TEXT_PUBLICATION', icon: "pi pi-book", title: "Text publication", description: 'Text publication' }],
    ['VIDEO', { code: 'VIDEO', icon: "pi pi-video", title: "Video", description: 'Video (YouTube, FB, Instagram, TV program...)' }],
    ['AUDIO', { code: 'AUDIO', icon: "pi pi-volume-up", title: "Audio", description: 'Audio podcast product' }],
    ['OTHER', { code: 'OTHER', icon: "pi pi-images", title: "Other", description: 'Other' }]
  ]);

  public static financial_types: LookupModel[] = [
    { code: 'NOT_SET', icon: "", title: "Discuss", description: 'I am open to discuss' },
    { code: 'I_WILL_PAY', icon: "", title: "Pay", description: 'I will pay' },
    { code: 'ONLY_FREE_OR_FREE_TO_TRY', icon: "", title: "Free", description: 'Only Free or Free to Try' }
  ];
}
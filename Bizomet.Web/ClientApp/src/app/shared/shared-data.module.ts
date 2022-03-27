import { UserRole } from "./models/user-role.model";
import { KeyValuePairModel } from './models/key-value-pair.model';

export class SharedData {
  public static all_roles: UserRole[] = [
    { key: "Talent", name: "Talent" },
    { key: "Uplifter", name: "Uplifter" },
    { key: "MediaAssistant", name: "Media Assistant" },
    { key: "Promoter", name: "Promoter" },
    { key: "Producer", name: "Producer" }
  ];

  public static interview_conditions: KeyValuePairModel[] = [
    { code: 'FREE_INTERVIEW', name: 'I will do an interview for free' },
    { code: 'I_CHARGE_INTERVIEW', name: 'I will charge money for my interview' },
    { code: 'I_PAY_INTERVIEW', name: 'I will pay if someone interviews me' },
    { code: 'I_COLOBORATE_FREE_INTERVIEW', name: 'I will collaborate free for an exchange interview' },
    { code: 'OTHER', name: 'Other (please describe)' }
  ];

  public static interview_results: KeyValuePairModel[] = [
    { code: 'TEXT_PUBLICATION', name: 'Text publication' },
    { code: 'VIDEO', name: 'Video (YouTube, FB, Instagram, TV programs...)' },
    { code: 'AUDIO', name: 'Audio podcast product' },
    { code: 'OTHER', name: 'Other (please describe)' }
  ];

  public static financial_types: KeyValuePairModel[] = [
    { code: 'NOT_SET', name: 'I am open to discuss' },
    { code: 'I_WILL_PAY', name: 'I will pay' },
    { code: 'ONLY_FREE_OR_FREE_TO_TRY', name: 'Only Free or Free to Try' }
  ];
}
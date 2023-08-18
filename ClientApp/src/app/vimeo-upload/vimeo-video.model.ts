import { VimeoVideoTranscode } from './vimeo-video-transcode.model';
import { VimeoVideoUpload } from './vimeo-video-upload.model';

export interface VimeoVideo {
  uri: string;
  name: string;
  description: string;
  type: string;
  link: string;
  upload: VimeoVideoUpload;
  transcode: VimeoVideoTranscode;
  playerEmbedUrl: string;
  show?: boolean;
}

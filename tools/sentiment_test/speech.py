import os
import azure.cognitiveservices.speech as speechsdk

def text_to_speech(input_text: str):
    # 使用名为 "SPEECH_KEY" 和 "SPEECH_REGION" 的环境变量
    speech_config = speechsdk.SpeechConfig(subscription=os.environ.get('SPEECH_KEY'),
                                           region=os.environ.get('SPEECH_REGION'))

    # 设置音频输出配置为默认扬声器
    audio_config = speechsdk.audio.AudioOutputConfig(use_default_speaker=True)

    # 设置语音合成的语言和发音
    speech_config.speech_synthesis_voice_name = 'zh-CN-XiaoxiaoNeural'

    # 初始化语音合成器
    speech_synthesizer = speechsdk.SpeechSynthesizer(speech_config=speech_config, audio_config=audio_config)

    speech_synthesis_result = speech_synthesizer.speak_text_async(input_text).get()

    # 检查语音合成的结果
    if speech_synthesis_result.reason == speechsdk.ResultReason.SynthesizingAudioCompleted:
        return f"文本 [{input_text}] 已转为语音"
    elif speech_synthesis_result.reason == speechsdk.ResultReason.Canceled:
        cancellation_details = speech_synthesis_result.cancellation_details
        if cancellation_details.reason == speechsdk.CancellationReason.Error:
            if cancellation_details.error_details:
                return f"语音合成被取消: {cancellation_details.reason}，错误详情: {cancellation_details.error_details}，您是否已设置语音资源的密钥和区域值？"
        else:
            return f"语音合成被取消: {cancellation_details.reason}"


if __name__ == '__main__':
    text = input("请输入你想转为语音的文本 >")
    result = text_to_speech(text)
    print(result)

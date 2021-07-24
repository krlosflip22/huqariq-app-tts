package com.itsigned.huqariq.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.FrameLayout
import androidx.fragment.app.Fragment
import com.itsigned.huqariq.R
import com.unity3d.player.UnityPlayer
import kotlinx.android.synthetic.main.activity_main_unity.view.*
import io.github.cdimascio.dotenv.dotenv



/**
 * A simple [Fragment] subclass.
 * Use the [UnityFragment.newInstance] factory method to
 * create an instance of this fragment.
 */
class UnityFragment : Fragment() {
    protected var mUnityPlayer: UnityPlayer? = null
    var frameLayoutForUnity: FrameLayout? = null
    var isAPIKeySetup : Boolean = false

    fun UnityFragment() {}

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        mUnityPlayer = UnityPlayer(activity)
        val view = inflater.inflate(R.layout.fragment_unity, container, false)
        frameLayoutForUnity =
            view.findViewById<View>(R.id.frame_layout_unity) as FrameLayout
        frameLayoutForUnity!!.addView(
            mUnityPlayer!!.view,
            FrameLayout.LayoutParams.MATCH_PARENT, FrameLayout.LayoutParams.MATCH_PARENT
        )
        mUnityPlayer!!.requestFocus()
        mUnityPlayer!!.windowFocusChanged(true)
        return view
    }

    public fun SendTTSText(textToSend : String){
        if(!isAPIKeySetup){
            val dotEnv = dotenv {
                directory = "/assets"
                filename = "env" // instead of '.env', use 'env'
            }
            UnityPlayer.UnitySendMessage("TTS", "SetTTSApiKey", dotEnv["TTS_API_KEY"] ?: "")
            UnityPlayer.UnitySendMessage("TTS", "SetRapidApiKey", dotEnv["RAPID_API_KEY"] ?: "")
            isAPIKeySetup = true
        }
        UnityPlayer.UnitySendMessage("TTS", "PlayVoice", textToSend)
    }

    override fun onDestroy() {
        mUnityPlayer!!.quit()
        super.onDestroy()
    }

    override fun onPause() {
        super.onPause()
        mUnityPlayer!!.pause()
    }

    override fun onResume() {
        super.onResume()
        mUnityPlayer!!.resume()
    }
}